using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class PickupableSkillData
{
    public float Progress { get; set; }
    public bool IsActivated { get; set; }
    public PickupableSkill PickupableSkill { get; set; }
}
