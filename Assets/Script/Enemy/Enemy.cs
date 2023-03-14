using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class Enemy : PoolObject
{
    public StateMachine StateMachine { get; private set; }
    public Animator Anim { get; set; }

    [FormerlySerializedAs("Enemy Data")] [SerializeField]
    public EnemyScriptableObject enemyData;

    [SerializeField] public Slider healthUI;
    [SerializeField] public Slider shieldUI;
    public SpriteRenderer Sprite { get; private set; }
    [SerializeField] public float StopAttackRange = 0f;

    private void OnEnable()
    {
        SetHealthValue();
    }

    public override void StartUp()
    {
        gameObject.SetActive(true);
    }

    public virtual void Awake()
    {
        StateMachine = new StateMachine();
        Health.OnHealthChanged += SetHealthValue;
        Shield.OnShieldChanged += SetShieldValue;
    }

    public virtual void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        StateMachine.Update();
    }

    private void SetHealthValue()
    {
        TryGetComponent<Health>(out var healthScript);
        healthUI.value = (float)healthScript.CurrentHealth / healthScript.maximumHealth;
    }

    private void SetShieldValue()
    {
        TryGetComponent<Shield>(out var shieldScript);
        shieldUI.value = (float)shieldScript.CurrentShield / shieldScript.maximumShield;
    }

    public virtual void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    public virtual void Attack()
    {
    }
}