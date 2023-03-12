using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public StatusEffectScriptableObject statusEffect;

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public void Initialize(StatusEffectScriptableObject statusEffect)
    {
        this.statusEffect = statusEffect;
    }

    public virtual void Reset()
    {
    }
}