using System;
using System.Collections.Generic;

public static class SkillUltilities
{
    public class PickupableSkillInformation
    {

        public Type ScriptType;
        public PickupableSkillType SkillType;
        public PooledObjectName PickupablePoolObjectName;

        public PickupableSkillInformation(Type type, PickupableSkillType skillType, PooledObjectName pickupablePoolObjectName)
        {
            ScriptType = type;
            SkillType = skillType;
            PickupablePoolObjectName = pickupablePoolObjectName;
        }
    }

    public static Dictionary<PickupableSkill, PickupableSkillInformation> SkillScriptTypeMap =
        new Dictionary<PickupableSkill, PickupableSkillInformation>()
    {
        {PickupableSkill.BulletStorm, new PickupableSkillInformation(
                typeof(BulletStormSkill),
                PickupableSkillType.Trigger,
                PooledObjectName.BulletStormPickupable
            )
        },
        {PickupableSkill.Disintegration, new PickupableSkillInformation(
                typeof(DisintegrationSkill),
                PickupableSkillType.Directional,
                PooledObjectName.DisintegrationPickupable
            )
        },
        {PickupableSkill.Piercing, new PickupableSkillInformation(
                typeof(PiercingSkill),
                PickupableSkillType.Trigger,
                PooledObjectName.PiercingPickupable
            )
        }
    };

    public static Type GetScriptType(this PickupableSkill skill)
    {
        return SkillScriptTypeMap[skill].ScriptType;
    }

    public static PickupableSkillType GetSkillType(this PickupableSkill skill)
    {
        return SkillScriptTypeMap[skill].SkillType;
    }

    public static PooledObjectName GetPOName(this PickupableSkill skill)
    {
        return SkillScriptTypeMap[skill].PickupablePoolObjectName;
    }
}