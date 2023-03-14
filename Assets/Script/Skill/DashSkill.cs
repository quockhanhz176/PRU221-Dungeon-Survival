using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DashSkill : ActivatableSkill
{
    public float Distance;
    public float Duration;
    public float CoolDown;
    public Rigidbody2D Rigidbody;
    public Action OnDashStart;
    public Action OnDashFinish;

    private bool _dashFinished = true;
    private Vector2 _direction;

    public override bool Activate()
    {
        return StartTrackingPoint(() =>
        {
            _direction = DirectionGetter.Invoke();
            StartDashing();
        });
    }

    public void Update()
    {
        UpdateTrackingPoint(point =>
        {
            if (point >= Duration)
            {
                StopDashing();
            }

            if (point >= Duration + CoolDown)
            {
                StopTrackingPoint();
            }
        });
    }

    public float GetCoolDownLeft()
    {
        return Mathf.Max(CoolDown + Duration - _currentPoint, 0);
    }

    public override object Export()
    {
        return new DashData
        {
            Progress = _currentPoint,
            IsTracking = _isTracking,
            Direction = _direction
        };
    }

    public override void Import(object o)
    {
        var data = (DashData)o;
        StopTrackingPoint();
        StopDashing();
        if (data.IsTracking)
        {
            StartTrackingPoint(null, data.Progress);
            if (data.Progress < Duration)
            {
                _direction = data.Direction;
                StartDashing();
                _currentPoint = data.Progress;
            }
        }
    }

    private void StartDashing()
    {
        OnDashStart.Invoke();
        Rigidbody.velocity = _direction.normalized * Distance / Duration;
        _dashFinished = false;
    }

    private void StopDashing()
    {
        if (!_dashFinished)
        {
            OnDashFinish.Invoke();
            Rigidbody.velocity = Vector2.zero;
            _dashFinished = true;
        }
    }
}
