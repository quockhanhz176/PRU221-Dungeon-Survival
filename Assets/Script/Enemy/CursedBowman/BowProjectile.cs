using System;
using UnityEngine;

public class BowProjectile : PoolObject
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDistance;
    private Vector2 _lastSpawn;
    private Vector2 _lastEnemyPosition;
    private Vector3 _projectileDir;

    public void Initialize(Vector2 lastSpawn, Vector2 lastEnemyPosition)
    {
        _lastSpawn = lastSpawn;
        _lastEnemyPosition = lastEnemyPosition;
        transform.position = _lastSpawn;
        _projectileDir = (_lastEnemyPosition - _lastSpawn).normalized;
        float angle = Mathf.Atan2(_projectileDir.y, _projectileDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void StartUp()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        // Debug.Log(Vector2.Distance(transform.position, _lastSpawn));
        if (Vector2.Distance(transform.position, _lastSpawn) > projectileDistance)
        {
            ReturnToPool();
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.position += _projectileDir * (Time.deltaTime * projectileSpeed);
    }

    public override PooledObjectName GetPoolObjectName()
    {
        return PooledObjectName.BowProjectile;
    }
}