using BrackeysJam.Weapons.Projectiles;
using UnityEngine;

namespace BrackeysJam.Weapons.Factory
{
    public class ProjectileFactory : IProjectileFactory
    {
        public IProjectile SpawnProjectile(Vector2 start, Vector2 end, GameObject projectilePrefab)
        {
            var projectileGameObject = Object.Instantiate(projectilePrefab);
            var projectile = projectileGameObject.GetComponent<IProjectile>();
            
            projectile.SetStartPosition(start);
            projectile.SetTargetPosition(end);

            return projectile;
        }
    }
}