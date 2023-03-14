using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BasicBulletFactory : IBulletFactory
{
    public GameObject CreateBullet()
    {
        return GameManager.Instance.ObjectPool.GetPooledObject(PooledObjectName.BasicBullet);
    }
}
