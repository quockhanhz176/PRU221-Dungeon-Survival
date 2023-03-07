using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PiercingBullet : PoolObject
{
    public override PooledObjectName GetPoolObjectName()
    {
        return PooledObjectName.PiercingBullet;
    }

    public override void StartUp()
    {
        gameObject.GetComponent<Destroyable>().ResetStartTime();
        gameObject.SetActive(true);
    }
}
