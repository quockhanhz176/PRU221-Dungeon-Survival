using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class PiercingPickupable : SkillPickupable
{
    protected override PickupableSkill GetPickupableSkill()
    {
        return GameManager.Instance.Player.GetComponentInChildren<PiercingSkill>();
    }
}
