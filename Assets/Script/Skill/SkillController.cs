using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public BasicShootSkill BasicShoot;
    public ProjectileLauncher Dash;
    public ProjectileLauncher ThirdSkill;

    private bool _isBasicShooting = false;
    void Awake()
    {
    }

    private void Update()
    {
        if (_isBasicShooting)
        {
            BasicShoot.Activate();
        }
    }

    public void SetBasicShooting(bool value)
    {
        _isBasicShooting = value;
    }
}
