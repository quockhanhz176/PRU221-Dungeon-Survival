using UnityEngine;

namespace Script.Enemy.Factories
{
    public class ChargerEnemyFactory : EnemyFactory
    {
        public GameObject CreateEnemy()
        {
            return GameManager.Instance.ObjectPool.GetPooledObject(PooledObjectName.Charger);
        }
    }
}