using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public delegate void PickupableSkillChange(PickupableSkillType oldType, PickupableSkillType newType);

    public BasicShootSkill BasicShoot;
    public DashSkill DashSkill;
    public ActivatableSkill PickupableSkill { get; private set; }
    public bool IsBasicShooting { get; private set; } = false;
    public bool IsDashing { get; private set; } = false;
    public PickupableSkillType PickupableSkillType { get; private set; } = PickupableSkillType.None;
    public event PickupableSkillChange OnPickupableSkillChange;
    public Joystick SkillJoystick;
    public GameObject SkillDirectionIndicator;
    public float SkillDirectionIndicatorDistance;

    private Dictionary<Type, Type> SkillStateDict = new Dictionary<Type, Type>
    {
        { typeof(BulletStormSkill), typeof(BulletStormState) },
        { typeof(PiercingSkill), typeof(PiercingState) },
        { typeof(DisintegrationSkill), typeof(DisintegrationState) },
    };
    private Action _onSkillFinishedDelegate;
    private Action _onSkillActivationFinishedDelegate;
    private Func<Vector2> _directionalSkilldirectionGetter;

    private void SetupSkills()
    {
        SetUpDash();
        SetUpBasicShoot();
        SetUpPickupableSkills();
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

    public void UsePickupableSkill(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            UsePickupableSkill();
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

        var newSkill = (ActivatableSkill)GetComponentInChildren(skill.GetScriptType());

        newSkill.OnSkillActivationFinished = _onSkillActivationFinishedDelegate;
        newSkill.OnSkillFinished = _onSkillFinishedDelegate;
        newSkill.Refresh();
        PickupableSkill = newSkill;
        var oldType = PickupableSkillType;
        PickupableSkillType = skill.GetSkillType();
        OnPickupableSkillChange?.Invoke(oldType, PickupableSkillType);
        if (PickupableSkillType == PickupableSkillType.Directional)
        {
            newSkill.DirectionGetter = _directionalSkilldirectionGetter;
        }
        return true;
    }

    private void SetUpDash()
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

    private void SetUpBasicShoot()
    {
        BasicShoot.SetBasicShootDirectionGetter(GetShootDirection);
    }

    private void SetUpPickupableSkills()
    {
        _onSkillFinishedDelegate = () =>
        {
            PickupableSkill = null;
            OnPickupableSkillChange?.Invoke(PickupableSkillType, PickupableSkillType.None);
        };
        _onSkillActivationFinishedDelegate = () => _psState = new NoSkillState(this);
        _directionalSkilldirectionGetter = () =>
        {
            var direction = new Vector2(SkillJoystick.Horizontal, SkillJoystick.Vertical);
            if (direction == Vector2.zero)
                direction = LookDirection;
            return direction;
        };
    }

    private void CheckSkillJoystick()
    {
        var direction = new Vector2(SkillJoystick.Horizontal, SkillJoystick.Vertical);
        if (direction == Vector2.zero)
        {
            SkillDirectionIndicator.gameObject.SetActive(false);
        }
        else
        {
            SkillDirectionIndicator.gameObject.SetActive(true);
            SkillDirectionIndicator.transform.position = transform.position + (Vector3)direction.normalized * SkillDirectionIndicatorDistance;
            SkillDirectionIndicator.transform.rotation = Quaternion.FromToRotation(Vector2.right, direction);
        }
    }

    /// <summary>
    /// Get the shoot direction. The shoot direction is the nearest enemy if there is one or more present in a radius of 20 surrounding the player
    /// or the direction the player is facing if there are none
    /// </summary>
    /// <param name="bulletRadius"></param>
    /// <param name="shootRange"></param>
    /// <returns></returns>
    private Vector2 GetShootDirection(float bulletRadius, float shootRange)
    {
        //find nearest enemy
        var colliders = Physics2D.OverlapCircleAll(transform.position, shootRange);
        Collider2D closetEnemy = null;
        var biggestDistance = float.MaxValue;
        foreach (var enemy in colliders.Where(c => c.gameObject.tag == "Enemy"))
        {
            //only consider enemy if there are no obstacle between player and the enemy
            var distance = (enemy.transform.position - transform.position).magnitude;
            int obstacle = LayerMask.GetMask("Obstacle");
            if (!Physics2D.CircleCast(
                transform.position,
                bulletRadius,
                enemy.transform.position - transform.position,
                distance,
                obstacle))
            {
                if (distance < biggestDistance)
                {
                    biggestDistance = distance;
                    closetEnemy = enemy;
                }
            }
        }

        return closetEnemy != null ?
            (closetEnemy.transform.position - transform.position) :
            GameManager.Instance.Player.LookDirection;
    }

}
