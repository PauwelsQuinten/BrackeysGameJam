using System.Collections.Generic;
using UnityEngine;

public class BiscuitSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _biscuitPrefabs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _burntBiscuitPrefabs = new List<GameObject>();

    private float _towerHeight = -3.54f;

    private void Start()
    {
        SpawnRandomBiscuit();
    }

    private void SpawnRandomBiscuit()
    {
        GameObject biscuitPrefab = SelectRandomBiscuit();

        Vector3 spawnPos = transform.position;
        spawnPos.y = _towerHeight + 10;

        GameObject spawnedBiscuit = Instantiate(biscuitPrefab, spawnPos, transform.rotation);
        spawnedBiscuit.transform.parent = this.transform;
        spawnedBiscuit.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private GameObject SelectRandomBiscuit()
    {
        int biscuitType = 0;
        int biscuitIndex = 0;
        biscuitType = Random.Range(1, 3);

        switch (biscuitType) 
        { 
            case 1:
                biscuitIndex = Random.Range(0, _biscuitPrefabs.Count);
                return _biscuitPrefabs[biscuitIndex];
            case 2:
                biscuitIndex = Random.Range(0, _burntBiscuitPrefabs.Count);
                return _burntBiscuitPrefabs[biscuitIndex];
        }

        return null;
    }
}
