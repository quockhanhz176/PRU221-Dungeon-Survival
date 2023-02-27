using UnityEngine;
using System.Collections;
using System;

public class Skill : MonoBehaviour
{
    public float coolDown;
    public float range;
    public float speed;
    public int projectTileCount = 1;
    public int spreadDegree;
    public GameObject projectilePrefab;
    public float _firePointOffset = 0.5f;

    private float _nextAvailableTime = -1;

    public void CastSkill(Transform firePoint, Func<Vector2> directionGetter)
    {
        if (Time.time < _nextAvailableTime)
        {
            return;
        }

        if (spreadDegree == 0)
        {
            spreadDegree = 1;
        }

        _nextAvailableTime = Time.time + coolDown;
        float totalSpread = Math.Abs(spreadDegree) * (projectTileCount - 1);
        var direction = directionGetter.Invoke();
        for (var degree = -totalSpread / 2; degree <= totalSpread / 2; degree += spreadDegree)
        {
            var projectileDirectionVector = (Quaternion.Euler(0, 0, degree) * direction).normalized;

            var projectileOffset = projectileDirectionVector * _firePointOffset;

            GameObject projectile = Instantiate(
                projectilePrefab,
                firePoint.position + projectileOffset,
                //Quaternion.LookRotation(projectileDirectionVector, Vector2.up)
                Quaternion.identity //TODO: make projectile rotate to make it's 'tail' point toward the player
                );

            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent?.SetDestroyAfter(range / speed);

            var projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody?.AddForce(Quaternion.Euler(0, 0, degree) * projectileDirectionVector * speed, ForceMode2D.Impulse);
        }
    }
}