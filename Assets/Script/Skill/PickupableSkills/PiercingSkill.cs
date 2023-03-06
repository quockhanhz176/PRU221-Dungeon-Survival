using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PiercingSkill : PickupableSkill
{
    public float Duration = 6;
    public float CoolDownScale = 0.666f;

    private bool _duringActivation = false;
    // time into skill activation
    private float _currentPoint = 0;
    private BasicShootSkill _basicShoot;
    private void Start()
    {
        _basicShoot = GameManager.Instance.Player.GetComponentInChildren<BasicShootSkill>();
    }

    private void Update()
    {
        if (_duringActivation)
        {
            _currentPoint += Time.deltaTime;
            //if finished
            if (_currentPoint >= Duration)
            {
                _duringActivation = false;
                _currentPoint = 0;
                _basicShoot.SetBulletFactory(new BasicBulletFactory());
                _basicShoot.CoolDown /= CoolDownScale;
                if (OnSkillActivationFinished != null)
                {
                    OnSkillActivationFinished.Invoke();

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
        _basicShoot.SetBulletFactory(new PiercingBulletFactory());
        _basicShoot.CoolDown *= CoolDownScale;
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
}
