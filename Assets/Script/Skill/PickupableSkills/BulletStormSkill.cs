using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BulletStormSkill : PickupableSkill
{
    [Tooltip("The spell duration, measured in seconds")]
    public float Duration = 2;
    public int Round = 20;
    public int ProjectileCount = 8;
    public int CoolDown = 0;
    [Tooltip("The speed with which the bullets travel (m/s)")]
    public float BulletSpeed = 10;
    public float Range = 3;
    public float HealingProportion = 0.1f;
    public int DamagePerBullet = 1;
    public float MovementSpeedMultiplier = 0.4f;

    private bool _duringActivation = false;
    // time into skill activation
    private float _currentPoint = 0;
    private int _roundFired = 0;
    private ProjectileLauncher ProjectileLauncher;
    private float _displacementAngle = 0;
    private float _accumulatedHeal = 0;
    private Health _playerHealth;

    private void Start()
    {
        Action<int> healDelegate = damage => AddAccumlatedHealth(damage * HealingProportion);
        _playerHealth = GameManager.Instance.Player.GetComponent<Health>();
        var pool = GameManager.Instance.ObjectPool;
        var spreadDegree = 360f / ProjectileCount;
        ProjectileLauncher = new ProjectileLauncher(() =>
        {
            var bullet = pool.GetPooledObject(PooledObjectName.BulletStormBullet);
            var damage = bullet.GetComponent<Damage>();
            damage.OnDameDealt = healDelegate;
            damage.damageAmount = DamagePerBullet;
            return bullet;
        });
        ProjectileLauncher.ProjectTileCount = ProjectileCount;
        ProjectileLauncher.SpreadDegree = spreadDegree;
        ProjectileLauncher.Speed = BulletSpeed;
        ProjectileLauncher.DestroyAfter = Range / BulletSpeed;
        _displacementAngle = spreadDegree / 2;
    }

    private void Update()
    {
        if (_duringActivation)
        {
            _currentPoint += Time.deltaTime;
            if (_currentPoint >= Duration / (Round - 1) * _roundFired)
            {
                var displacementAngle = _roundFired % 2 == 0 ? 0 : _displacementAngle;
                ProjectileLauncher.Launch(transform.position, Vector2.up, displacementAngle);
                _roundFired++;

                //if finished
                if (_roundFired == Round)
                {
                    _duringActivation = false;
                    _roundFired = 0;
                    _currentPoint = 0;
                    if (OnSkillActivationFinished != null)
                    {
                        OnSkillActivationFinished.Invoke();
                    }
                }
            }
        }
    }
    public override bool Activate()
    {
        if (_duringActivation)
        {
            return false;
        }

        _duringActivation = true;
        return true;
    }

    public override float GetActivationLeft()
    {
        if (_duringActivation)
        {
            return Duration - _currentPoint;
        }
        else
        {
            return 0;
        }
    }

    private void AddAccumlatedHealth(float amount)
    {
        _accumulatedHeal += amount;
        if (_accumulatedHeal > 1)
        {
            var healAmount = Mathf.FloorToInt(_accumulatedHeal);
            _playerHealth.ReceiveHealing(healAmount);
            _accumulatedHeal -= healAmount;
        }
    }
}
