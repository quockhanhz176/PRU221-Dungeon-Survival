using UnityEngine;

[CreateAssetMenu(fileName = "newStatusEffect", menuName = "Status Effect/New Status Effect")]
public class StatusEffectScriptableObject : ScriptableObject
{
    [Range(0, 100)] public int occuredChance;
    public int effectDamage;
    public float effectDuration;
    public float effectTickDuration;
    public bool movementImpaired;
    [Range(0, 100)] public int movementImpairedPercent;
    [HideInInspector] public float currentDuration;
    [HideInInspector] public float currentTick;

    public void ResetData()
    {
        currentDuration = 0;
        currentTick = 0;
    }
}