using BrackeysJam.Weapons.Projectiles;
using UnityEngine;

namespace BrackeysJam.Weapons.Factory
{
    public interface IProjectileFactory
    {
        public IProjectile SpawnProjectile(Vector2 start, Vector2 end, GameObject projectilePrefab);
    }
}