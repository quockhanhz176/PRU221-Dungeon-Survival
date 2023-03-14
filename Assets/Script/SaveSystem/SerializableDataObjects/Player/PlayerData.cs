using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public Vector3 Position { get; set; }
    public Vector3 LookDirection { get; set; }
    public int CurrentHealth { get; set; }
    public object BasicShootData { get; set; }
    public object DashData { get; set; }
    public PickupableSkillData PickupableSkillData { get; set; }
}

