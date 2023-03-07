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
                _skill._gun.RayActive = false;
                _skill._gun.ReturnToPool();
                _skill.OnSkillActivationFinished?.Invoke();
                if (--_skill._roundLeft == 0)
                {
                    _skill.OnSkillFinished?.Invoke();
                }
                _skill.StopTrackingPoint();
                _skill._gunState = new NoGunState(_skill);
            }
        }
    }
}
