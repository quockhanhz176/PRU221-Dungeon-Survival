using System;
using UnityEngine;

public class BowProjectile : MonoBehaviour
{
    private Vector2 _lastSpawn;
    private Vector2 _lastEnemyPosition;
    private Vector2 _projectileDir;


    public void Initialize(Vector2 lastSpawn, Vector2 lastEnemyPosition)
    {
        _lastSpawn = lastSpawn;
        _lastEnemyPosition = lastEnemyPosition;
    }

    private void Start()
    {
        _projectileDir = (_lastSpawn - _lastEnemyPosition).normalized;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        
    }
}