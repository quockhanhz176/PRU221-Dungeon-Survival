using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BulletStormBullet : PoolObject
{
    public override PooledObjectName GetPoolObjectName()
    {
        return PooledObjectName.BulletStormBullet;
    }
    public override void StartUp()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Destroyable>().ResetStartTime();
        transform.rotation = Quaternion.identity;
    }
}
