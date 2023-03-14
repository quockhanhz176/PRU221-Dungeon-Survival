using System;
using UnityEngine;

public class Poison : StatusEffect
{
    private Health _health;

    public override void Start()
    {
        _health = this.GetComponent<Health>();
    }

    public override void Update()
    {
        if (statusEffect.currentDuration >= statusEffect.effectDuration)
        {
            Destroy(this);
        }

        statusEffect.currentTick += Time.deltaTime;
        if (statusEffect.currentTick >= statusEffect.effectTickDuration)
        {
            _health.TakeDamage(statusEffect.effectDamage);
            statusEffect.currentDuration += statusEffect.currentTick;
            statusEffect.currentTick = 0;
        }
    }

    public override void Reset()
    {
        statusEffect.currentDuration = 0;
        statusEffect.currentTick = 0;
    }

    public void OnDestroy()
    {
        statusEffect.ResetData();
    }
}