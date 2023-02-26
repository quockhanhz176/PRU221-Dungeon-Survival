using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject destroyEffect;

    private float _destroyAfter;

    private float _startTime;

    public void SetDestroyAfter(float time)
    {
        _destroyAfter = time;
    }

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        if (_destroyAfter == 0) return;
        if(Time.time - _startTime >= _destroyAfter)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject != null)
            Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (gameObject != null)
            Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation, null);
        }
    }
}
