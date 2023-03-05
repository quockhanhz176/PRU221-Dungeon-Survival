using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public BasicShootSkill BasicShoot;
    public DashSkill DashSkill;
    public PickupableSkill ThirdSkill { get; private set; }

    private bool _isBasicShooting = false;
    private Action _remove3rdSkillDelegate;
    private Player _player;

    void Start()
    {
        SetUpDash();
        _remove3rdSkillDelegate = () =>
        {
            ThirdSkill = null;
            _player.IsUsingBulletStorm = false;
        };
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (_isBasicShooting && _player.IsUsingBulletStorm == false)
        {
            BasicShoot.Activate();
        }
    }

    public void SetBasicShooting(bool value)
    {
        _isBasicShooting = value;
    }

    public void Dash()
    {
        DashSkill.Activate();
    }

    public void UseThirdSkill()
    {
        if (ThirdSkill != null)
        {
            var result = ThirdSkill.Activate();
            if (result && ThirdSkill.GetType() == typeof(BulletStormSkill))
            {
                _player.IsUsingBulletStorm = true;
            }
        }
    }

    /// <summary>
    /// Submit the third skill to the skill controller
    /// </summary>
    /// <returns>Whether the skill was accepted</returns>
    public bool SubmitThirdSkill(PickupableSkill skill)
    {
        if (ThirdSkill != null && ThirdSkill.GetActivationLeft() > 0)
        {
            return false;
        }

        skill.OnSkillActivationFinished = _remove3rdSkillDelegate;
        ThirdSkill = skill;
        return true;
    }

    private void SetUpDash()
    {
        var player = GameManager.Instance.Player;
        var trailRenderer = player.GetComponent<TrailRenderer>();
        DashSkill.OnDashStart = () =>
        {
            player.IsDashing = true;
            trailRenderer.emitting = true;
        };
        DashSkill.OnDashFinish = () =>
        {
            player.IsDashing = false;
            trailRenderer.emitting = false;
        };
        DashSkill.DirectionGetter = () => player.LookDirection;
    }
}
