using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class DisintegrationSkill
{
    private class GunDisappearingState : DisintegrationSkillGunState
    {
        private float _stateFinishPoint;
        public GunDisappearingState(DisintegrationSkill skill) : base(skill)
        {
            _stateFinishPoint = skill.GunAppearDuration + skill.ShootDuration + skill.GunDisappearDuration;
        }

        public override void Update(float point)
        {
            var alpha = (_stateFinishPoint - point) / _skill.GunAppearDuration;
            _skill._gun.SetAlpha(alpha);

            if (alpha <= 0)
            {
                _skill.StopShooting();
            }
        }
    }
}
