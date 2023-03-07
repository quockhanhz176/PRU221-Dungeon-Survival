using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public GameObject destroyEffect;

    [Tooltip("The number of second after which the object will be destroyed." +
        " Set to 0 or smaller to not destroy the object after a certain amount of time")]
    public float DestroyAfter;

    public bool DestroyOnCollision = true;

    private float _startTime;

    public void Start()
    {
        _startTime = Time.time;
    }

    public void Update()
    {
        if (DestroyAfter <= 0) return;
        if (Time.time - _startTime >= DestroyAfter)
        {
            Destroy();
        }
    }

    public void ResetStartTime()
    {
        _startTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DestroyOnCollision) Destroy();
    }

    private void Destroy()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
        var poolObject = GetComponent<PoolObject>();
        if (poolObject != null)
        {
            GameManager.Instance.ObjectPool.ReturnPooledObject(poolObject.GetPoolObjectName(), gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
