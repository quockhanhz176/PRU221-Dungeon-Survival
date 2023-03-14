using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class DisintegrationSkill : ActivatableSkill
{
    public int Rounds = 3;
    public float GunAppearDuration = 0.3f;
    public float ShootDuration = 0.7f;
    public float GunDisappearDuration = 0.7f;
    public float DistanceBehindPlayer = 3f;
    public float GunPushBackMax = 3;

    private int _roundLeft = 3;
    private DisintegrationGun _gun;
    private Vector2 _direction = Vector2.right;
    private Player _player;
    private Func<float, float> _rayWidthFunction;
    private Func<float, float> _pushBackFunction;
    private DisintegrationSkillGunState _gunState;

    private void Start()
    {
        _gunState = new NoGunState(this);
        _player = GameManager.Instance.Player;
        _rayWidthFunction = point => Mathf.Sin(point / ShootDuration * Mathf.PI);
        _pushBackFunction = point => Mathf.Sin(point / ShootDuration * Mathf.PI * 2 / 3);
    }

    private void Update()
    {
        UpdateTrackingPoint(point =>
        {
            _gunState.Update(point);
        });
    }

    public override void Refresh()
    {
        _roundLeft = Rounds;
    }

    public override bool Activate()
    {
        return StartTrackingPoint(() =>
        {
            _direction = DirectionGetter.Invoke().normalized;
            SetupGun();
        });
    }

    public override float GetActivationLeft()
    {
        return 0;
    }

    public override object Export()
    {
        return new DisintegrationData
        {
            IsActivated = _isTracking,
            Progress = _currentPoint,
            RoundsLeft = _roundLeft,
            Direction = _direction,
            PickupableSkill = PickupableSkill.Disintegration
        };
    }

    public override void Import(object dataObject)
    {
        if (_isTracking)
        {
            StopShooting();
        }

        var o = (DisintegrationData)dataObject;
        _roundLeft = o.RoundsLeft;
        if (o.IsActivated)
        {
            StartTrackingPoint(() =>
            {
                _direction = o.Direction;
                SetupGun();
            }, o.Progress);
        }
    }

    private void SetupGun()
    {
        _gun = GameManager.Instance.ObjectPool.GetPooledObject(PooledObjectName.DisintegrationGun).GetComponent<DisintegrationGun>();
        _gun.gameObject.transform.position = _player.transform.position + (Vector3)_direction * -DistanceBehindPlayer;
        _gun.gameObject.transform.rotation *= Quaternion.FromToRotation(Vector2.right, _direction);
        _gunState = new GunAppearingState(this);
    }

    private void StopShooting()
    {
        _gun.RayActive = false;
        _gun.ReturnToPool();
        OnSkillActivationFinished?.Invoke();
        if (--_roundLeft == 0)
        {
            OnSkillFinished?.Invoke();
        }
        StopTrackingPoint();
        _gunState = new NoGunState(this);
    }
}
