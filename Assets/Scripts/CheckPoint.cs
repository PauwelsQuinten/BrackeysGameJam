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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Biscuit") return;
        _plate.SetActive(true);
        _tableCollider.enabled = true;
        _resetPenalties.Raise(this, EventArgs.Empty);
    }
}
