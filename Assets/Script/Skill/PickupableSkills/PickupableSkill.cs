using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PickupableSkill : MonoBehaviour, ActivatableSkill
{
    public abstract bool Activate();

    /// <summary>
    /// Get the amount of time of skill activation left. 
    /// </summary>
    /// <returns>The amount of time of skill activation left. Returns 0 if the skill is not activated</returns>
    public abstract float GetActivationLeft();

    public Action OnSkillActivationFinished;
}
