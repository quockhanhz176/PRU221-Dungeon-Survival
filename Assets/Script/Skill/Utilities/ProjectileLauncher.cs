using UnityEngine;
using System.Collections;
using System;

public class ProjectileLauncher
{
    public float Range = 5;
    public float Speed = 5;
    public int ProjectTileCount = 1;
    public int SpreadDegree = 0;
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
            //TODO: rotate projectile so its 'tail' point toward the player

            var destroyable = projectile.GetComponent<Destroyable>();
            if(destroyable != null)
            {
                destroyable.DestroyAfter = Range / Speed;
            }
            destroyable.Start();

            var projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = Quaternion.Euler(0, 0, degree) * projectileDirectionVector * Speed;
        }
    }
}