﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PickupData : PoolObjectData
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
}
