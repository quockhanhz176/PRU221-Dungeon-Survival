using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public BasicShootSkill BasicShoot;
    public DashSkill DashSkill;
    public ProjectileLauncher ThirdSkill;

    private bool _isBasicShooting = false;
    void Awake()
    {
        SetUpDash();
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

    private void Update()
    {
        if (_isBasicShooting)
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
}
