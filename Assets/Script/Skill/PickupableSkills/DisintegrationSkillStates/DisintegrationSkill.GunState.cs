using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class DisintegrationSkillGunState
{
    protected DisintegrationSkill _skill;

    public DisintegrationSkillGunState(DisintegrationSkill skill)
    {
        _skill = skill;
    }

    public abstract void Update(float point);
}
