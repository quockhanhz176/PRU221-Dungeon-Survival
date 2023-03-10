using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ActivatableSkill : MonoBehaviour, IExportable<object>
{
    public Func<Vector2> DirectionGetter = () => Vector2.right;
    /// <summary>
    /// There can be multiple skill activations (rounds). Callback 
    /// <c>OnSkillActivationFinished</c> is called after each skill activation.
    /// </summary>
    public Action OnSkillActivationFinished;
    public Action OnSkillFinished;

    protected bool _isTracking = false;
    // time into skill activation
    protected float _currentPoint = 0;
    protected bool _justActivated = false;

    /// <summary>
    /// Activate the skill
    /// </summary>
    /// <returns>Whether the activation was successful</returns>
    public virtual bool Activate() => false;

    /// <summary>
    /// Get the amount of time of skill activation left. 
    /// </summary>
    /// <returns>The amount of time of skill activation left. Returns 0 if the skill is not activated</returns>
    public virtual float GetActivationLeft() => 0;

    /// <summary>
    /// Refresh the skill after an use (when the skill is "acquired" by player)
    /// </summary>
    public virtual void Refresh() { }

    /// <summary>
    /// Start tracking the point into activation and perform onPointStart if tracking hasn't been started
    /// </summary>
    /// <param name="onSuccess">Action to be performed if start tracking was successful</param>
    protected bool StartTrackingPoint(Action onSuccess = null, float currentPoint = 0)
    {
        if (_isTracking)
        {
            return false;
        }

        _isTracking = true;
        _justActivated = true;
        _currentPoint = currentPoint;
        onSuccess?.Invoke();
        return true;
    }

    /// <summary>
    /// Update tracking point and perform action based on current point if point is being tracked
    /// </summary>
    /// <param name="onPointUpdate">Action performed every frame</param>
    protected void UpdateTrackingPoint(Action<float> onPointUpdate = null)
    {
        if (_isTracking)
        {
            if (_justActivated)
            {
                _justActivated = false;
            }
            else
            {
                _currentPoint += Time.deltaTime;
            }

            onPointUpdate?.Invoke(_currentPoint);
        }
    }

    protected void StopTrackingPoint()
    {
        _isTracking = false;
    }

    public abstract object Export();

    public abstract void Import(object o);
}

