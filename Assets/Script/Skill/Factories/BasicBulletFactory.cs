using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BasicBulletFactory : BulletFactory
{
    public GameObject CreateBullet()
    {
        return GameManager.Instance.ObjectPool.GetPooledObject(PooledObjectName.BasicBullet);
    }
}
