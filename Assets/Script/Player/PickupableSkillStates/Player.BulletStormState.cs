using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Player
{
    class BulletStormState : PickupableSkillState
    {
        private float _speed;
        public BulletStormState(Player player) : base(player)
        {
            var multiplier = ((BulletStormSkill)_player.PickupableSkill).MovementSpeedMultiplier;
            _speed = multiplier * _player.Speed;
        }

        public override void Dash()
        {
            _player.DashSkill.Activate();
        }

        public override void Move()
        {
            _player.Move(_speed);
        }

        public override void Shoot()
        {
        }
    }
}

