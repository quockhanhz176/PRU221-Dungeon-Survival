using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InvincibleHealthHandler : HealthHandler
{
    public override int DeductHealth(int health)
    {
        return 0;
    }
}