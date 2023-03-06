using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public float Speed = 10;
    public Joystick Joystick;
    public Vector2 LookDirection { get; private set; } = Vector2.right;

    private Rigidbody2D _rigidBody;
    private PickupableSkillState _psState;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _psState = new NoSkillState(this);
        SetupSkills();
    }

    void Update()
    {
        if (!IsDashing)
        {
            _psState.Move();
        }
        ShootUpdate();
    }

    private void Move(float speed)
    {
        var direction = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        if (direction != Vector2.zero)
            LookDirection = direction;
        _rigidBody.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDashing)
        {
            _rigidBody.velocity = Vector2.zero;
        }
    }
}
