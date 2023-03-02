using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BasicBullet : MonoBehaviour, PoolObject
{
    public PooledObjectName GetPoolObjectName()
    {
        return PooledObjectName.BasicBullet;
    }

    public void StartUp()
    {
        gameObject.SetActive(true);
    }

    public void Initialize()
    {
    }
}
