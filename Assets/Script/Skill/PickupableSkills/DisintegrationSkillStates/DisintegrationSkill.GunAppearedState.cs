using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class DisintegrationSkill
{
    private class GunAppearedState : DisintegrationSkillGunState
    {
        private Vector2 _initialPosition;

        public GunAppearedState(DisintegrationSkill skill) : base(skill)
        {
            _initialPosition = skill._gun.transform.position;
        }

        public override void Update(float point)
        {
            // shoot
            if (_skill._gun.RayActive == false)
            {
                _skill._gun.RayActive = true;
            }

            var currentPoint = point - _skill.GunAppearDuration;
            _skill._gun.SetRayWidth(_skill._rayWidthFunction(currentPoint));
            _skill._gun.transform.position =
                _initialPosition + (_skill._pushBackFunction(currentPoint) * _skill.GunPushBackMax * -_skill._direction);

            // check finish
            if (point >= _skill.GunAppearDuration + _skill.ShootDuration)
            {
                _skill._gunState = new GunDisappearingState(_skill);
                _skill._gunState.Update(point);
            }
        }
    }
}
