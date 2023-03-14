using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class DashData
{
    public float Progress { get; set; }
    public bool IsTracking { get; set; }
    public Vector3 Direction { get; set; }
}