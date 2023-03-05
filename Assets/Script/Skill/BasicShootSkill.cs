using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BasicShootSkill : MonoBehaviour, ActivatableSkill
{
    public float CoolDown = 0.5f;

    public float Speed = 30;

    public float Range = 20;

    public ProjectileLauncher ProjectileLauncher;

    public BulletFactory BulletFactory { get; private set; }

    private float _nextActivatableTime = 0;

    private float _bulletRadius;

    public void Start()
    {
        ProjectileLauncher = new ProjectileLauncher(() => BulletFactory.CreateBullet());
        ProjectileLauncher.Speed = Speed;
        ProjectileLauncher.DestroyAfter = Range / Speed;
        SetBulletFactory(new BasicBulletFactory());
    }

    public void SetBulletFactory(BulletFactory bulletFactory)
    {
        BulletFactory = bulletFactory;
        UpdateSpecs();
    }

    public bool Activate()
    {
        if (GetCoolDownLeft() > 0) return false;

        ProjectileLauncher.Launch(transform.position, getDirection());
        _nextActivatableTime = Time.time + CoolDown;
        return true;
    }

    // the shoot direction is the nearest enemy if there is one or more present in a radius of 20 surrounding the player
    // or the direction the player is facing if there are none
    private Vector2 getDirection()
    {
        //find nearest enemy
        var colliders = Physics2D.OverlapCircleAll(transform.position, Range);
        Collider2D closetEnemy = null;
        var biggestDistance = float.MaxValue;
        foreach (var enemy in colliders.Where(c => c.gameObject.tag == "Enemy"))
        {
            //only consider enemy if there are no obstacle between player and the enemy
            var distance = (enemy.transform.position - transform.position).magnitude;
            int obstacle = LayerMask.GetMask("Obstacle");
            if (!Physics2D.CircleCast(
                transform.position,
                _bulletRadius,
                enemy.transform.position - transform.position,
                distance,
                obstacle))
            {
                if (distance < biggestDistance)
                {
                    biggestDistance = distance;
                    closetEnemy = enemy;
                }
            }
        }

        return closetEnemy != null ?
            (closetEnemy.transform.position - transform.position) :
            GameManager.Instance.Player.LookDirection;
    }

    public float GetCoolDownLeft()
    {
        return Mathf.Max(_nextActivatableTime - Time.time, 0);
    }

    private void UpdateSpecs()
    {
        var projectile = BulletFactory.CreateBullet();
        _bulletRadius = projectile.transform.localScale.x;
        var poolObject = projectile.GetComponent<PoolObject>();
        if (poolObject != null)
        {
            GameManager.Instance.ObjectPool.ReturnPooledObject(poolObject.GetPoolObjectName(), projectile);
        }
        else
        {
            Destroy(projectile);
        }
    }
}

