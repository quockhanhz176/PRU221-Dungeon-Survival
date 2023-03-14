using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NightWolf : Enemy
{
    [FormerlySerializedAs("_statusEffect")] [SerializeField]
    private StatusEffectScriptableObject statusEffect;

    public override PooledObjectName GetPoolObjectName()
    {
        return PooledObjectName.NightWolf;
    }

    public override void StartUp()
    {
        TryGetComponent<Shield>(out var shield);
        shield.enabled = true;
        shield.ResetState();
        TryGetComponent<Health>(out var health);
        health.Reset();
        gameObject.SetActive(true);
    }

    public override void Awake()
    {
        Anim = GetComponent<Animator>();
        base.Awake();
    }

    public override void Start()
    {
        StateMachine.ChangeState(new NightWolfIdleState(this));
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

    public override void Attack()
    {
        GameManager.Instance.Player.GetComponent<Health>().TakeDamage(enemyData.damage);
        var random = Random.Range(0, 101);
        if (random <= statusEffect.occuredChance)
        {
            if (!GameManager.Instance.Player.TryGetComponent<Poison>(out var t))
            {
                GameManager.Instance.Player.gameObject.AddComponent<Poison>();
                GameManager.Instance.Player.GetComponent<Poison>().Initialize(statusEffect);
            }
            else
            {
                GameManager.Instance.Player.GetComponent<Poison>().Reset();
            }
        }
    }

    private void OnDisable()
    {
        StateMachine.ChangeState(new NightWolfIdleState(this));
        // enemyData.ResetData();
    }
}