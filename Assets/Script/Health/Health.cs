using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The <c>Health</c> class use the Chain of Responsibility design pattern to handle health
/// </summary>
public class Health : MonoBehaviour
{
    [Header("Team Settings")]
    [Tooltip("The team associated with this damage")]
    public int teamId = 0;

    [Header("Health Settings")]
    [Tooltip("The maximum health value")]
    public int maximumHealth = 1;
    [Tooltip("The current in game health value")]
    [SerializeField]
    private int _currentHealth = 1;
    public int CurrentHealth
    {
        get => _currentHealth;
        private set => _currentHealth = value;
    }
    [Tooltip("Invulnerability duration, in seconds, after taking damage")]
    public float invincibilityTime = 1f;

    #region Health handler
    private IHealthHandler _healthHandler;

    public void AddHealthHandler(IHealthHandler handler)
    {
        handler.SetNext(_healthHandler);
        _healthHandler = handler;
    }
    //return true if the handler is found and removed, false otherwise
    public bool RemoveHealthHandler(IHealthHandler handler)
    {
        if (_healthHandler == handler)
        {
            _healthHandler = _healthHandler.GetNext();
            return true;
        }
        else
        {
            var previousHandler = _healthHandler;
            while (_healthHandler.GetNext() != null)
            {
                if (_healthHandler.GetNext() == handler)
                {
                    _healthHandler.SetNext(handler.GetNext());
                    return true;
                }
            }
            return false;
        }
    }
    #endregion

    private void Start()
    {
        _healthHandler = new DefaultHealthHandler(this);
    }

    /// <summary>
    /// Deal damage to this health. The damage taken can be different from the damage dealt, depending on buffs and debuffs.
    /// </summary>
    /// <param name="damageAmount">The damage dealt</param>
    /// <returns>The damage taken</returns>
    public int TakeDamage(int damageAmount) => _healthHandler.DeductHealth(damageAmount);

    /// <summary>
    /// Heal this health. The healing received can be different from the healing dealt, depending on buffs and debuffs.
    /// </summary>
    /// <param name="healingAmount">The healing dealt</param>
    /// <returns>The healing received</returns>
    public int ReceiveHealing(int healingAmount) => _healthHandler.AddHealth(healingAmount);

    [Header("Effects & Polish")]
    [Tooltip("The effect to create when this health dies")]
    public GameObject deathEffect;
    [Tooltip("The effect to create when this health is damaged (but does not die)")]
    public GameObject hitEffect;

    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation, null);
        }
        Destroy(gameObject);
    }
    private IEnumerator BeInvincible()
    {
        if (invincibilityTime < 0) yield break;

        var invincibleHandler = new InvincibleHealthHandler();
        AddHealthHandler(invincibleHandler);
        yield return new WaitForSeconds(invincibilityTime);
        RemoveHealthHandler(invincibleHandler);
    }

    public class DefaultHealthHandler : HealthHandler
    {
        private Health _outer;

        public DefaultHealthHandler(Health health)
        {
            _outer = health;
        }

        public override int AddHealth(int health)
        {
            if (health <= 0) return 0;

            var actualHealingAmount = Mathf.Min(health, _outer.maximumHealth - _outer.CurrentHealth);
            _outer.CurrentHealth += actualHealingAmount;
            return actualHealingAmount;
        }
        public override int DeductHealth(int health)
        {
            if (health <= 0) return 0;

            var actualLosingAmount = Mathf.Min(health, _outer.CurrentHealth);
            _outer.CurrentHealth -= actualLosingAmount;
            if (_outer.hitEffect != null)
            {
                Instantiate(_outer.hitEffect, _outer.transform.position, _outer.transform.rotation, null);
            }
            if (_outer.CurrentHealth <= 0)
            {
                _outer.Die();
            }
            else
            {
                //Become invincible for a certain time after being damaged, the duration depends on the setting
                _outer.StartCoroutine(_outer.BeInvincible());
            }
            return actualLosingAmount;
        }

    }
}
