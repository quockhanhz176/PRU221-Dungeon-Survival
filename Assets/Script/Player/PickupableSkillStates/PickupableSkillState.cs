using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The state the player is in regarding the pickupable skill
/// </summary>
public abstract class PickupableSkillState
{
    protected Player _player;
    public PickupableSkillState(Player player)
    {
        _player = player;
    }

    public abstract void Dash();

    public abstract void Move();

    public abstract void Shoot();
}
