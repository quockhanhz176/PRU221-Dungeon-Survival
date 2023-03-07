using UnityEngine;
using System.Collections;
using System;

public class ProjectileLauncher
{
    [Tooltip("Time(seconds) after which projectile will be destroyed if it has Destroyable script attached")]
    public float DestroyAfter = 1;
    public float Speed = 5;
    public int ProjectTileCount = 1;
    public float SpreadDegree = 0;
    public float FirePointOffset = 0.5f;

    public Func<GameObject> ProjectileGetter;

    public ProjectileLauncher(Func<GameObject> projectileGetter)
    {
        ProjectileGetter = projectileGetter;
    }

    public void Launch(Vector2 firePoint, Vector2 direction, float angleDisplacement = 0)
    {
        int count = ProjectTileCount;
        float totalSpread = Math.Abs(SpreadDegree) * (ProjectTileCount - 1);
        for (var projectileNo = 1; projectileNo <= count; projectileNo++)
        {
            var degree = -totalSpread / 2 + angleDisplacement + (projectileNo - 1) * SpreadDegree;
            var projectileDirectionVector = (Quaternion.Euler(0, 0, degree) * direction).normalized;
            var projectileOffset = projectileDirectionVector * FirePointOffset;

            GameObject projectile = ProjectileGetter.Invoke();
            projectile.transform.position = firePoint + (Vector2)projectileOffset;
            projectile.transform.rotation *= Quaternion.FromToRotation(Vector2.right, projectileDirectionVector);

            var destroyable = projectile.GetComponent<Destroyable>();
            if (destroyable != null && DestroyAfter > 0)
            {
                destroyable.DestroyAfter = DestroyAfter;
            }

            var projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = projectileDirectionVector * Speed;
        }
    }
}