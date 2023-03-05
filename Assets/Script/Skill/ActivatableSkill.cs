using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ActivatableSkill
{
    /// <summary>
    /// Activate the skill
    /// </summary>
    /// <returns>Whether the activation was successful</returns>
    public bool Activate();
}

