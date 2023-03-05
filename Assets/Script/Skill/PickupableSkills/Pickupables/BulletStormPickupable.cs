using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class BulletStormPickupable : SkillPickupable
{
    protected override PickupableSkill GetPickupableSkill()
    {
        return GameManager.Instance.Player.GetComponentInChildren<BulletStormSkill>();
    }
}
