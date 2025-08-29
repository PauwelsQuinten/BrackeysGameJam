using UnityEngine;

public class GarbageBin : MonoBehaviour
{
    [SerializeField]
    private GameEvent AddStrike;

    private BiscuitSpawner _spawner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Biscuit") return;

        if (_spawner == null) _spawner = FindObjectOfType<BiscuitSpawner>();

        GameObject biscuit = collision.gameObject;
        BiscuitBehaviour bis = biscuit.GetComponent<BiscuitBehaviour>();

        if (bis == null) return;

        if (bis.HasTouchedPlate)
            _spawner.RemoveBiscuit(bis);
        else _spawner.SpawnRandomBiscuit();

        Destroy(biscuit);
    }
}
