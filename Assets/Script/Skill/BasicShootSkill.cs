using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BasicShootSkill : ActivatableSkill
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
        ProjectileLauncher = new ProjectileLauncher(() =>
        {
            return BulletFactory.CreateBullet();
        });
        ProjectileLauncher.Speed = Speed;
        ProjectileLauncher.DestroyAfter = Range / Speed;
        SetBulletFactory(new BasicBulletFactory());
    }

    public void SetBulletFactory(BulletFactory bulletFactory)
    {
        BulletFactory = bulletFactory;
        UpdateSpecs();
    }

    public override bool Activate()
    {
        if (GetCoolDownLeft() > 0) return false;

        ProjectileLauncher.Launch(transform.position, DirectionGetter.Invoke());
        _nextActivatableTime = Time.time + CoolDown;
        return true;
    }

    public float GetCoolDownLeft()
    {
        return Mathf.Max(_nextActivatableTime - Time.time, 0);
    }

    public void SetBasicShootDirectionGetter(BasicShootDirectionGetter getter)
    {
        DirectionGetter = () => getter.Invoke(_bulletRadius, Range);
    }

    private void UpdateSpecs()
    {
        var projectile = BulletFactory.CreateBullet();
        _bulletRadius = projectile.transform.localScale.x;
        var poolObject = projectile.GetComponent<PoolObject>();
        if (poolObject != null)
        {
            poolObject.ReturnToPool();
        }
        else
        {
            Destroy(projectile);
        }
    }

    public delegate Vector2 BasicShootDirectionGetter(float bulletRadius, float range);
}

