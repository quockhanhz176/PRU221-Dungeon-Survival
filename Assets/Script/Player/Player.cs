using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10;
    public Joystick Joystick;
    public Vector2 LookDirection { get; private set; } = Vector2.right;

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var direction = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        if (direction != Vector2.zero)
            LookDirection = direction;
        _rigidBody.velocity = direction * Speed;
    }
}
