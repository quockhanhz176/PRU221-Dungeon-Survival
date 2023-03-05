using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    /// <summary>
    /// Initialize pool object the first time they are created (initiated from prefab).
    /// </summary>
    public virtual void Initialize() { }

    /// <summary>
    /// Startup the object before being sent out from the object pool. 
    /// The gameObject this script is attached to is inactive at this point and should be set to active if necessary.
    /// </summary>
    public virtual void StartUp() { }

    public abstract PooledObjectName GetPoolObjectName();

    /// <summary>
    /// Return itself to the object pool
    /// </summary>
    public virtual void ReturnToPool()
    {
        GameManager.Instance.ObjectPool.ReturnPooledObject(GetPoolObjectName(), gameObject);
    }
}
