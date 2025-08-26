using System.Collections;
using UnityEngine;

public class BiscuitBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _fallSpeed = 1;
    private bool _hasCollided;
    void Start()
    {
        StartCoroutine(MoveDown());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag != "Biscuit") return;
        _hasCollided = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private IEnumerator MoveDown()
    {
        while (!_hasCollided)
        {
            transform.position -= transform.up * _fallSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
