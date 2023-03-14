using UnityEngine;

namespace Script.Enemy.Factories
{
    public class NightWolfEnemyFactory : EnemyFactory
    {
        public GameObject CreateEnemy()
        {
            return GameManager.Instance.ObjectPool.GetPooledObject(PooledObjectName.NightWolf);
        }
    }
}