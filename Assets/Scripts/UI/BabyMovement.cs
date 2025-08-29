using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BabyMovement : MonoBehaviour
{
    public float Distance = 1.0f, VerticalSpeed = 1.0f, HorizontalSpeed = 2.0f;
    [SerializeField]
    private Transform _startPosition, _endposition;
    private float _journeyLength;
    private SpriteRenderer[] _spritesRenderer;
    private bool isFacingRight = true;

    private void Start()
    {
        _startPosition.position = transform.position;

        _journeyLength = Mathf.Abs(_endposition.position.x - _startPosition.position.x);

        _spritesRenderer = GetComponentsInChildren<SpriteRenderer>();

        if (_spritesRenderer == null)
        {
            Debug.Log("No SpriteRenderer found");
            return;
        }
    }

    private void Update()
    {
        float newY = _startPosition.position.y + Mathf.Sin(Time.time * VerticalSpeed) * (Distance / 2);

        float newX = _startPosition.position.x + Mathf.PingPong(Time.time * HorizontalSpeed, _journeyLength);

        float threshold = 0.1f;
        if (Mathf.Abs(newX - _startPosition.position.x) < threshold && !isFacingRight)
        {
            transform.rotation *= transform.rotation * Quaternion.Euler(0,0,0);
            isFacingRight = true;
        }
        else if (Mathf.Abs(newX - _endposition.position.x) < threshold && isFacingRight)
        {
            transform.rotation *= transform.rotation * Quaternion.Euler(0, 180, 0);
            isFacingRight = false;
        }

            transform.position = new Vector3(newX, newY, _startPosition.position.z);
    }

    private void FlipSprites(bool spriteDirection)
    {
        foreach (SpriteRenderer sprite in _spritesRenderer) 
        { 
            sprite.flipX = spriteDirection;
        }
    }
}
