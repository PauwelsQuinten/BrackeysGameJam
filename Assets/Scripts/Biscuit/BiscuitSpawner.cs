using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BiscuitSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _biscuitPrefabs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _burntBiscuitPrefabs = new List<GameObject>();
    [SerializeField]
    private float _biscuitMinSize = 0.5f;
    [SerializeField]
    private float _biscuitMaxSize = 1f;
    [SerializeField]
    private GameEvent _towerGrew;
    [SerializeField]
    private GameEvent _biscuitAdded;
    [SerializeField]
    private GameEvent _biscuitDestroyed;
    [SerializeField]
    private GameEvent _takePenalty;

    private float _towerHeight = 0;
    private List<GameObject> _biscuits = new List<GameObject>();

    private void Start()
    {
        SpawnRandomBiscuit();
    }

    public void SpawnRandomBiscuit()
    {
        _towerGrew.Raise(this, _towerHeight);
        GameObject biscuitPrefab = SelectRandomBiscuit();

        Vector3 spawnPos = transform.position;
        spawnPos.y = _towerHeight + 9;

        GameObject spawnedBiscuit = Instantiate(biscuitPrefab, spawnPos, transform.rotation);
        spawnedBiscuit.transform.parent = this.transform;
        spawnedBiscuit.GetComponent<Rigidbody2D>().gravityScale = 0;
        spawnedBiscuit.GetComponent<BiscuitBehaviour>().CurrentTowerHight = _towerHeight;
        float scale = UnityEngine.Random.Range(_biscuitMinSize, _biscuitMaxSize);
        spawnedBiscuit.transform.localScale = new Vector3(scale, scale, scale);
    }

    private GameObject SelectRandomBiscuit()
    {
        int isBurned = 0;
        int biscuitType = 0;
        int biscuitIndex = 0;
        isBurned = UnityEngine.Random.Range(1, 4);

        if (isBurned == 2) biscuitType = 2;
        else biscuitType = 1;

        switch (biscuitType) 
        { 
            case 1:
                biscuitIndex = UnityEngine.Random.Range(0, _biscuitPrefabs.Count);
                return _biscuitPrefabs[biscuitIndex];
            case 2:
                biscuitIndex = UnityEngine.Random.Range(0, _burntBiscuitPrefabs.Count);
                return _burntBiscuitPrefabs[biscuitIndex];
        }

        return null;
    }

    private void MakeBurntBiscuits(Vector2 originPos)
    {
        List<Collider2D> biscuits = Physics2D.OverlapCircleAll(originPos, 1).ToList();

        foreach(Collider2D collider in biscuits)
        {
            BiscuitBehaviour biscuit = collider.gameObject.GetComponent<BiscuitBehaviour>();
            if(biscuit == null) continue;
            RemoveBiscuit(biscuit);
            biscuit.Burn();
            if (_biscuits.Count > 0)
            {
                if (_biscuits[0].GetComponent<BiscuitBehaviour>().IsBurned) _takePenalty.Raise(this, EventArgs.Empty);
            }
        }

    }

    public void TowerCollisionDetected(Component sender, object obj)
    {
        GameObject collisionObj = obj as GameObject;
        BiscuitBehaviour biscuit = sender.gameObject.GetComponent<BiscuitBehaviour>();
        Vector3 newPiecePos = sender.transform.position;
        if(_towerHeight <= newPiecePos.y)
        {
            _towerHeight = newPiecePos.y;
            _biscuits.Add(biscuit.gameObject);
            _towerGrew.Raise(this, _towerHeight);
            if(!biscuit.IsBurned) _biscuitAdded.Raise(this, EventArgs.Empty);
        }
        if (biscuit.IsBurned && collisionObj.GetComponent<BiscuitBehaviour>() != null)
            MakeBurntBiscuits(sender.gameObject.transform.position);
        else if (biscuit.IsBurned)
        {
            biscuit.Burn();

            if (_biscuits.Count > 0)
            {
                if (_biscuits[0].GetComponent<BiscuitBehaviour>().IsBurned) _takePenalty.Raise(this, EventArgs.Empty);
            }

            RemoveBiscuit(biscuit);
        }

        biscuit.enabled = false;
        SpawnRandomBiscuit();
    }

    public void RemoveBiscuit(BiscuitBehaviour biscuit)
    {
        _biscuits.Remove(biscuit.gameObject);
        if (_biscuits.Count - 1 >= 0)
        {
            if(_biscuits[_biscuits.Count - 1] != null) _towerHeight = _biscuits[_biscuits.Count - 1].transform.position.y;
        }
        else _towerHeight = 0;
        _towerGrew.Raise(this, _towerHeight);
        if(!biscuit.IsBurned)_biscuitDestroyed.Raise(this, EventArgs.Empty);
    }
}
