using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _plate;
    [SerializeField]
    private BoxCollider2D _tableCollider;
    [SerializeField]
    private GameEvent _resetPenalties;

    private float _towerHeight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Biscuit") return;
        if (collision.gameObject.GetComponent<BiscuitBehaviour>().IsBurned) return;
        if (_towerHeight < transform.position.y) return;
        GetComponent<BoxCollider2D>().enabled = false;
        _plate.SetActive(true);
        _tableCollider.enabled = true;
        _resetPenalties.Raise(this, EventArgs.Empty);
    }

    public void TowerHeightChanged(Component sender, object obj)
    {
        BiscuitBehaviour biscuit = obj as BiscuitBehaviour;
        float newHeight = 0;
        if (biscuit != null)
        {
            newHeight = biscuit.transform.position.y;
        }
        else newHeight = (float)obj;
        _towerHeight = newHeight;
    }
}
