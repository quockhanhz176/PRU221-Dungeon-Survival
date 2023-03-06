using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Player
{
    class PiercingState : PickupableSkillState
    {
        public PiercingState(Player player) : base(player)
        {
        }

        public override void Dash()
        {
            _player.DashSkill.Activate();
        }

        public override void Move()
        {
            _player.Move(_player.Speed);
        }

        public override void Shoot()
        {
            _player.BasicShoot.Activate();
        }
    }
}

