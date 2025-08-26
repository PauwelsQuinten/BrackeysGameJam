using UnityEngine;

public class GarbageBin : MonoBehaviour
{
    private GameEvent AddStrike;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Biscuit") return;

        GameObject biscuit = collision.gameObject;
        BiscuitBehaviour bis = biscuit.GetComponent<BiscuitBehaviour>();

        if (bis == null) return;

        if (bis.IsBurned)
        {
            Destroy(biscuit);
        }else if (!bis.IsBurned)
        {
            AddStrike.Raise(this);
            Destroy(biscuit);
        }

    }
}
