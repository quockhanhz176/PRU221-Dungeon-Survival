using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PiercingSkill : ActivatableSkill
{
    public float Duration = 6;
    public float CoolDownScale = 0.666f;
    private BasicShootSkill _basicShoot;
    private void Start()
    {
        _basicShoot = GameManager.Instance.Player.GetComponentInChildren<BasicShootSkill>();
    }

    private void Update()
    {
        UpdateTrackingPoint(point =>
        {
            //if finished
            if (_currentPoint >= Duration)
            {
                StopTrackingPoint();
                _basicShoot.SetBulletFactory(new BasicBulletFactory());
                _basicShoot.CoolDown /= CoolDownScale;
                OnSkillActivationFinished?.Invoke();
                OnSkillFinished?.Invoke();
            }
        });
    }

    public override bool Activate()
    {
        return StartTrackingPoint(() =>
        {
            _basicShoot.SetBulletFactory(new PiercingBulletFactory());
            _basicShoot.CoolDown *= CoolDownScale;
        });
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
}
