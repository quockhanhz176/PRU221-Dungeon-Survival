using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PickupableSkill
{
    BulletStorm,
    Disintegration,
    Piercing
}

//[Serializable]
//public sealed class PickupableSkill
//{
//    public static readonly PickupableSkill BulletStorm = new PickupableSkill(typeof(BulletStormSkill));
//    public static readonly PickupableSkill Disintegration = new PickupableSkill(typeof(DisintegrationSkill));
//    public static readonly PickupableSkill Piercing = new PickupableSkill(typeof(PiercingSkill));

//    public Type SkillType;

//    private PickupableSkill(Type type)
//    {
//        SkillType = type;
//    }
//}
