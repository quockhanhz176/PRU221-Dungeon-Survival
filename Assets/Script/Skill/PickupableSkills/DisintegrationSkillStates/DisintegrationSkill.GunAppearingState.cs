using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class DisintegrationSkill
{
    private class GunAppearingState : DisintegrationSkillGunState
    {
        public GunAppearingState(DisintegrationSkill skill) : base(skill)
        {
        }

        public override void Update(float point)
        {
            var alpha = Mathf.Min(point / _skill.GunAppearDuration, 1);
            _skill._gun.SetAlpha(alpha);
            if (alpha >= 1)
            {
                _skill._gunState = new GunAppearedState(_skill);
                _skill._gunState.Update(point);
            }
        }
    }
}
