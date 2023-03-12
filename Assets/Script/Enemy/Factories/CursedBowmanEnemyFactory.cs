using UnityEngine;

namespace Script.Enemy.Factories
{
    public class CursedBowmanEnemyFactory : EnemyFactory
    {
        public GameObject CreateEnemy()
        {
            return GameManager.Instance.ObjectPool.GetPooledObject(PooledObjectName.CursedBowman);
        }
    }
}