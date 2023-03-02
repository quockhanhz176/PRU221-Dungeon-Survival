using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ActivatableSkill
{
    public void Activate();

    public float GetCoolDownLeft();
}

