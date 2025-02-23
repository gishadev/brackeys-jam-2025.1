using System.Linq;
using BrackeysJam.PlayerController;
using BrackeysJam.Weapons.Factory;
using BrackeysJam.Weapons.Projectiles;
using UnityEngine;

namespace BrackeysJam.Weapons.ConcreteWeapons
{
    public abstract class ProjectileWeapon : WeaponBase
    {
        protected IProjectileFactory _projectileFactory;

        private Collider2D[] _colliders = new Collider2D[1];

        public override void Equip(IPlayerStats stats)
        {
            base.Equip(stats);
            _projectileFactory = new ProjectileFactory();
        }

        public override void Use()
        {
            GetTarget();
            FireProjectile();
        }

        private void GetTarget()
        {
            _colliders[0] = null;
            Physics2D.OverlapCircleNonAlloc(transform.position, WeaponData.Range, _colliders, LayerMask.GetMask("Enemy"));
        }

        protected virtual void FireProjectile()
        {
            var projectile = _effectsPool.FirstOrDefault(e => !e.gameObject.activeSelf)?.GetComponent<IProjectile>();
            
            if (projectile == null)
            {
                projectile = _projectileFactory.SpawnProjectile(transform.position, _colliders[0]?.transform.position ?? transform.forward, WeaponData.Effect.gameObject);
                _effectsPool.Add(projectile.transform.GetComponent<ParticleSystem>());
            }

            projectile.SetStartPosition(transform.position);
            projectile.SetTargetPosition(_colliders[0]?.transform.position ?? transform.forward);

            projectile.OnHit += Damage;
            
            projectile.Run();
            DisableEffect(projectile.transform.GetComponent<ParticleSystem>()).Forget();
        }
    }
}