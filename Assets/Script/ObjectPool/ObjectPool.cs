using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Provides object pooling for bullets and enemies
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public struct PoolObjectType
    {
        public PooledObjectName objName;
        [Tooltip("The prefab that the pool will return when the above name is supplied. " +
            "There should be a PoolObject implementation script attached to it")]
        public GameObject prefab;
        public int initialCapacity;
    }

    public List<PoolObjectType> PoolObjectTypes;

    private Dictionary<PooledObjectName, GameObject> _prefabs;

    private Dictionary<PooledObjectName, List<GameObject>> _pools;

    public void Awake()
    {
        // initialize dictionaries
        _pools = new Dictionary<PooledObjectName, List<GameObject>>();
        _prefabs = new Dictionary<PooledObjectName, GameObject>();

        foreach (var type in PoolObjectTypes)
        {
            // check pool object script present
            if (type.prefab.GetComponent<PoolObject>() == null)
            {
                throw new Exception("The prefab supplied to ObjectPool must have PoolObject implementation component");
            }

            _pools.Add(type.objName, new List<GameObject>(type.initialCapacity));
            _prefabs.Add(type.objName, type.prefab);

            // fill bullet pool
            var objPool = _pools[type.objName];
            for (int i = 0; i < type.initialCapacity; i++)
            {
                objPool.Add(GetNewObject(type.prefab));
            }
        }

    }

    /// <summary>
    /// Gets a pooled object from the pool
    /// </summary>
    /// <returns>pooled object</returns>
    /// <param name="name">name of the pooled object to get</param>
    public GameObject GetPooledObject(PooledObjectName name)
    {
        List<GameObject> pool = _pools[name];

        GameObject obj;
        // check for available object in pool
        if (pool.Count > 0)
        {
            // remove object from pool and return
            obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        }
        else
        {
            // pool empty, so expand pool and return new object
            pool.Capacity++;
            obj = GetNewObject(_prefabs[name]);
        }
        obj.GetComponent<PoolObject>().StartUp();
        return obj;
    }

    /// <summary>
    /// Returns a pooled object to the pool
    /// </summary>
    /// <param name="name">name of pooled object</param>
    /// <param name="obj">object to return to pool</param>
    public void ReturnPooledObject(PooledObjectName name, GameObject obj)
    {
        obj.SetActive(false);
        _pools[name].Add(obj);
    }

    /// <summary>
    /// Gets a new object
    /// </summary>
    /// <returns>new object</returns>
    private GameObject GetNewObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.GetComponent<PoolObject>().Initialize();

        obj.SetActive(false);
        //GameObject.DontDestroyOnLoad(obj);
        return obj;
    }

    /// <summary>
    /// Removes all the pooled objects from the object _pools
    /// </summary>
    public void EmptyPools()
    {
        // add your code here
        foreach (var pool in _pools)
        {
            pool.Value.RemoveAll(obj => true);
        }
    }
}
