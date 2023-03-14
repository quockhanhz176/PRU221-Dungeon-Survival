using System;
using UnityEngine;

public class CursedBowman : Enemy
{
    public override void StartUp()
    {
        TryGetComponent<Health>(out var health);
        health.Reset();
        gameObject.SetActive(true);
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        StateMachine.ChangeState(new CursedBowmanIdleState(this));
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override PooledObjectName GetPoolObjectName()
    {
        return PooledObjectName.CursedBowman;
    }

    private void OnDisable()
    {
        StateMachine.ChangeState(new CursedBowmanIdleState(this));
    }

    public override void Attack()
    {
        
    }
}