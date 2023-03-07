using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class DisintegrationPickupable : SkillPickupable
{
    protected override ActivatableSkill GetPickupableSkill()
    {
        return GameManager.Instance.Player.GetComponentInChildren<DisintegrationSkill>();
    }
}
