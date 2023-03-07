using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// The pickupable that lies on the ground and can be picked up by the player
/// </summary>
public abstract class SkillPickupable : PoolObject
{
    public override PooledObjectName GetPoolObjectName()
    {
        return PooledObjectName.BulletStormPickupable;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var gameObject = collision.gameObject;
        if (gameObject.tag == "Player")
        {
            var result = gameObject.GetComponent<Player>().SubmitPickupableSkill(GetPickupableSkill());
            if (result) ReturnToPool();
        }
    }

    protected abstract ActivatableSkill GetPickupableSkill();
}
