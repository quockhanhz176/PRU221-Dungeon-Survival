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
    public IBulletFactory BulletFactory { get; private set; }

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

    private void Update()
    {
        UpdateTrackingPoint(point =>
        {
            if (point >= CoolDown)
            {
                StopTrackingPoint();
            }
        });
    }

    public void SetBulletFactory(IBulletFactory bulletFactory)
    {
        BulletFactory = bulletFactory;
        UpdateSpecs();
    }

    public override bool Activate()
    {
        return StartTrackingPoint(() =>
        {
            ProjectileLauncher.Launch(transform.position, DirectionGetter.Invoke());
        });
    }

    public float GetCoolDownLeft()
    {
        if (_isTracking) return 0;
        return Mathf.Max(CoolDown - _currentPoint, 0);
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

    public override object Export()
    {
        return new BasicShootData
        {
            CoolDownLeft = CoolDown - _currentPoint
        };
    }

    public override void Import(object o)
    {
        var data = (BasicShootData)o;
        StartTrackingPoint(null, CoolDown - data.CoolDownLeft);
    }

    public delegate Vector2 BasicShootDirectionGetter(float bulletRadius, float range);
}

