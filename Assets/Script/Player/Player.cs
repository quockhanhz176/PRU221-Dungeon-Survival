using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10;
    public Joystick Joystick;
    public Vector2 LookDirection { get; private set; } = Vector2.right;

    public bool IsDashing = false;

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsDashing) return;

        var direction = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        if (direction != Vector2.zero)
            LookDirection = direction;
        _rigidBody.velocity = direction * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDashing)
        {
            _rigidBody.velocity = Vector2.zero;
        }
    }
}
