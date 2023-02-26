using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public Skill BasicShoot;
    public Skill Dash;
    public Skill ThirdSkill;

    private bool _isBasicShooting = false;
    private float _basicShootRange;
    private float _basicShootBulletRadius;
    void Awake()
    {
        BasicShoot = Instantiate(BasicShoot);
        _basicShootRange = BasicShoot.range;
        _basicShootBulletRadius = BasicShoot.projectilePrefab.transform.localScale.x;
        //Dash = Instantiate(Dash);
    }

    private void Update()
    {
        if (_isBasicShooting)
        {
            UseBasicShoot();
        }
    }

    private void UseBasicShoot()
    {
        // the shoot direction is the nearest enemy if there is one or more present in a radius of 20 surrounding the player
        // or the direction the player is facing if there are none
        Vector2 getDirection()
        {
            //find nearest enemy
            var colliders = Physics2D.OverlapCircleAll(transform.position, 20);
            Collider2D closetEnemy = null;
            var biggestDistance = float.MaxValue;
            foreach (var enemy in colliders.Where(c => c.gameObject.tag == "Enemy"))
            {
                //only consider enemy if there are no obstacle between player and the enemy
                var distance = (enemy.transform.position - transform.position).magnitude;
                int obstacle = LayerMask.GetMask("Obstacle");
                if (!Physics2D.CircleCast(
                    transform.position,
                    _basicShootBulletRadius,
                    enemy.transform.position - transform.position,
                    distance,
                    obstacle))
                {
                    if (distance < biggestDistance)
                    {
                        biggestDistance = distance;
                        closetEnemy = enemy;
                    }
                }
            }

            return closetEnemy != null ?
                (closetEnemy.transform.position - transform.position) :
                GameManager.Instance.Player.LookDirection;
        }

        BasicShoot.CastSkill(this.transform, getDirection);
    }

    public void SetBasicShooting(bool value)
    {
        _isBasicShooting = value;
    }
}
