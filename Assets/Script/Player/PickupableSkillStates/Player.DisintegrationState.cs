using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player
{
    class DisintegrationState : PickupableSkillState
    {
        public DisintegrationState(Player player) : base(player)
        {
        }

        public override void Dash()
        {
        }

        public override void Move()
        {
            _player._rigidBody.velocity = Vector2.zero;
        }

        public override void Shoot()
        {
        }
    }
}

