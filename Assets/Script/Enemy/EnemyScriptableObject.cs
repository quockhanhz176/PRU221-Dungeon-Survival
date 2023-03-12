using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData",menuName = "Enemy/Enemy Data")]
public class EnemyScriptableObject : ScriptableObject
{
    [InspectorName("Health")] public int health;
    [InspectorName("Shield")] public int shield;
    [InspectorName("Movement Speed")] public int moveSpeed;
    [InspectorName("Attack Damage")] public int damage;
    [InspectorName("Attack Speed")] public float atkSpeed;
    [InspectorName("Detection Range")] public float detectionRange;
    [InspectorName("Attack Range")] public float atkRange;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int currentShield;

    public void ResetData()
    {
        currentHealth = health;
        currentShield = shield;
    }
}