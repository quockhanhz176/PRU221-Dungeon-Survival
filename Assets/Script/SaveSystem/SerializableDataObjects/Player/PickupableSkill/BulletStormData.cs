using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class BulletStormData : PickupableSkillData
{
    public int RoundsFired { get; set; }
    public float AccumulatedHealth { get; set; }
}
