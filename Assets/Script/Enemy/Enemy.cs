using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class Enemy : PoolObject
{
    public StateMachine StateMachine { get; private set; }
    public Animator Anim { get; private set; }

    [FormerlySerializedAs("Enemy Data")] [SerializeField]
    public EnemyScriptableObject enemyData;

    [SerializeField] public Slider _healthUI;
    [SerializeField] public Slider _shieldUI;
    [SerializeField] public GameObject _shieldUIGO;
    public static event Action OnDeath;
    public SpriteRenderer Sprite { get; private set; }
    public GameObject Player { get; private set; }

    protected virtual void OnHit(int damage)
    {
        if (enemyData.currentHealth <= 0)
        {
            OnDeath?.Invoke();
            // GameManager.Instance.ObjectPool.ReturnPooledObject(PooledObjectName.NightWolf, gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        StateMachine = new StateMachine();
    }

    public virtual void Start()
    {
        Player = GameObject.Find("Player");
        Sprite = GetComponent<SpriteRenderer>();
        _healthUI.value = CalculateHealth();
        _shieldUI.value = CalculateShield();
    }

    public virtual void Update()
    {
        StateMachine.Update();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    protected float CalculateHealth()
    {
        return (float)enemyData.currentHealth / enemyData.health;
    }

    protected float CalculateShield()
    {
        return (float)enemyData.currentShield / enemyData.shield;
    }

    public override PooledObjectName GetPoolObjectName()
    {
        return PooledObjectName.NightWolf;
    }
}