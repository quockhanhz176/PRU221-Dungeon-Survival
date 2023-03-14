using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class DisintegrationData : PickupableSkillData
{
    public int RoundsLeft { get; set; }
    public Vector3 Direction { get; set; }
}
