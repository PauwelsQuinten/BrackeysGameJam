using System;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    private GameEvent _takePenalty;

    private BiscuitSpawner _spawner;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_spawner == null)_spawner = FindObjectOfType<BiscuitSpawner>();
        BiscuitBehaviour biscuit = collision.gameObject.GetComponent<BiscuitBehaviour>();
        if (biscuit == null) return;
        _takePenalty.Raise(this, EventArgs.Empty);
        Destroy(collision.gameObject);
        if(!biscuit.HasTouchedPlate) _spawner.SpawnRandomBiscuit();
    }
}
