using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class DisintegrationSkill
{
    private class NoGunState : DisintegrationSkillGunState
    {
        public NoGunState(DisintegrationSkill skill) : base(skill)
        {
        }

        public override void Update(float point)
        {
        }
    }
}
