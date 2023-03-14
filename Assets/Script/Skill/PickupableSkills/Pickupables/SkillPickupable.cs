using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// The pickupable that lies on the ground and can be picked up by the player
/// </summary>
public class SkillPickupable : PoolObject
{
    public PickupableSkill skill;
    public override PooledObjectName GetPoolObjectName()
    {
        return skill.GetPOName();
    }

    public override void StartUp()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var gameObject = collision.gameObject;
        if (gameObject.tag == "Player")
        {
            var result = gameObject.GetComponent<Player>().SubmitPickupableSkill(skill);
            if (result) ReturnToPool();
        }
    }
}
