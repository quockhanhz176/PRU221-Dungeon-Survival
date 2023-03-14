using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PiercingBulletFactory : IBulletFactory
{
    public GameObject CreateBullet() => GameManager.Instance.ObjectPool.GetPooledObject(PooledObjectName.PiercingBullet);
}
