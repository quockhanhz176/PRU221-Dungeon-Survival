using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NightWolf : Enemy
{
    [FormerlySerializedAs("_statusEffect")] [SerializeField]
    private StatusEffectScriptableObject statusEffect;

    protected override void OnHit(int damage)
    {
        if (enemyData.currentShield > 0)
        {
            enemyData.currentShield = Mathf.Clamp(enemyData.currentShield - damage, 0, enemyData.shield);
            _shieldUI.value = CalculateShield();
            _shieldUIGO.SetActive(enemyData.currentShield != 0);
            return;
        }

        enemyData.currentHealth = Mathf.Clamp(enemyData.currentHealth - damage, 0, enemyData.health);
        _healthUI.value = CalculateHealth();

        base.OnHit(damage);
    }

    public override void Awake()
    {
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

    public void Attack()
    {
        Player.GetComponent<Health>().TakeDamage(enemyData.damage);
        var random = Random.Range(0, 101);
        if (random <= statusEffect.occuredChance)
        {
            if (!Player.TryGetComponent<StatusEffect>(out var t))
            {
                Player.AddComponent<Poison>();
                Player.GetComponent<Poison>().Initialize(statusEffect);
            }
            else
            {
                Player.GetComponent<Poison>().Reset();
            }
        }
    }

    private void OnDisable()
    {
        enemyData.ResetData();
    }
}