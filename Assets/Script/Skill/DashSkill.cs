using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DashSkill : MonoBehaviour, ActivatableSkill
{
    public float Distance;

    public float Duration;

    public float CoolDown;

    public Rigidbody2D Rigidbody;

    public Func<Vector2> DirectionGetter = () => Vector2.right;

    public Action OnDashStart;

    public Action OnDashFinish;

    private float _coolDownLeft = 0;

    public void Activate()
    {
        if(_coolDownLeft <= 0)
        {
            StartCoroutine(Dash());
        }
    }

    public float GetCoolDownLeft()
    {
        return _coolDownLeft;
    }

    private IEnumerator Dash()
    {
        // start dash
        if (OnDashStart != null)
        {
            OnDashStart.Invoke();
        }
        var startTime = Time.time;
        var direction = DirectionGetter.Invoke();
        Rigidbody.velocity = direction.normalized * Distance / Duration;

        //during dash
        while (Time.time - startTime < Duration)
        {
            yield return null;
        }

        //after dash
        if (OnDashFinish != null)
        {
            OnDashFinish.Invoke();
        }
        Rigidbody.velocity = Vector2.zero;
        _coolDownLeft = CoolDown;

        //wait for cooldown
        var coolDownFinishTime = startTime + Duration + CoolDown;
        while (_coolDownLeft > 0)
        {
            _coolDownLeft = coolDownFinishTime - Time.time;
            yield return null;
        }
        _coolDownLeft = 0;
    }
}
