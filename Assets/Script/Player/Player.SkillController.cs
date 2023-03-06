using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public BasicShootSkill BasicShoot;
    public DashSkill DashSkill;
    public PickupableSkill PickupableSkill { get; private set; }
    public bool IsBasicShooting { get; private set; } = false;
    public bool IsDashing { get; private set; } = false;

    private Dictionary<Type, Type> SkillStateDict = new Dictionary<Type, Type>
    {
        { typeof(BulletStormSkill), typeof(BulletStormState) },
        { typeof(PiercingSkill), typeof(PiercingState) },
    };
    private Action _onSkillFinishDelegate;

    private void SetupSkills()
    {
        SetUpDash();
        _onSkillFinishDelegate = () =>
        {
            PickupableSkill = null;
            _psState = new NoSkillState(this);
        };
    }

    private void ShootUpdate()
    {
        if (IsBasicShooting)
        {
            _psState.Shoot();
        }
    }

    public void SetBasicShooting(bool value)
    {
        IsBasicShooting = value;
    }

    public void Dash()
    {
        _psState.Dash();
    }

    public void UsePickupableSkill()
    {
        if (PickupableSkill != null)
        {
            var result = PickupableSkill.Activate();
            if (result)
            {
                var stateType = SkillStateDict[PickupableSkill.GetType()];
                _psState = (PickupableSkillState)Activator.CreateInstance(stateType, new object[] { this });
            }
        }
    }

    /// <summary>
    /// Submit the pickupable skill to the skill controller
    /// </summary>
    /// <returns>Whether the skill was accepted</returns>
    public bool SubmitPickupableSkill(PickupableSkill skill)
    {
        if (PickupableSkill != null && PickupableSkill.GetActivationLeft() > 0)
        {
            return false;
        }

        skill.OnSkillActivationFinished = _onSkillFinishDelegate;
        PickupableSkill = skill;
        return true;
    }

    void SetUpDash()
    {
        var trailRenderer = GetComponent<TrailRenderer>();
        DashSkill.OnDashStart = () =>
        {
            IsDashing = true;
            trailRenderer.emitting = true;
        };
        DashSkill.OnDashFinish = () =>
        {
            IsDashing = false;
            trailRenderer.emitting = false;
        };
        DashSkill.DirectionGetter = () => LookDirection;
    }
}
