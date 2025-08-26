using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BiscuitBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _fallSpeed = 1;
    [SerializeField]
    private GameEvent _collidedWithTower;
    [SerializeField]
    private GameEvent _lowerThanTower;
    [SerializeField]
    private GameEvent _biscuitDestroyed;
    [SerializeField]
    private Sprite _burntTexture;
    [SerializeField]
    private SpriteRenderer _texture;

    private bool _hasCollided;
    private bool _isMoving;
    private Vector2 _movement;

    public bool IsBurned;
    public float CurrentTowerHight;
    public bool HasTouchedPlate;
    void Start()
    {
        StartCoroutine(MoveDown());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasCollided) return;
        if (collision.transform.gameObject.tag != "Biscuit") return;
        _hasCollided = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        Destroy(GetComponent<PlayerInput>());
        _collidedWithTower.Raise(this, collision.gameObject);
        HasTouchedPlate = true;
    }

    public void MoveX(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _isMoving = true;
            _movement = ctx.ReadValue<Vector2>();
            StartCoroutine(MoveX());
        }
        if (ctx.canceled) _isMoving = false;
    }

    public void MoveDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) _fallSpeed *= 2.5f;
        if(ctx.canceled) _fallSpeed = 1;
    }

    public void Burn()
    {
        _texture.sprite = _burntTexture;
        StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator MoveDown()
    {
        while (!_hasCollided)
        {
            transform.position -= transform.up * _fallSpeed * Time.deltaTime;
            if (CurrentTowerHight > transform.position.y) _lowerThanTower.Raise(this, transform.position.y);
            yield return null;
        }
    }

    private IEnumerator MoveX()
    {
        while (_isMoving)
        {
            transform.position += transform.right * _movement.x * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(1);
        _biscuitDestroyed.Raise(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }
}
