using BrackeysJam.Weapons.Factory;
using UnityEngine;

namespace BrackeysJam.Weapons.ConcreteWeapons
{
    public abstract class ProjectileWeapon : WeaponBase
    {
        protected IProjectileFactory _projectileFactory;

        private Collider2D[] _colliders = new Collider2D[1];

        public override void Equip()
        {
            base.Equip();
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
            var projectile = _projectileFactory.SpawnProjectile(transform.position, _colliders[0]?.transform.position ?? transform.forward, WeaponData.Effect.gameObject);
            projectile.Run();
            DisableEffect(projectile.transform.GetComponent<ParticleSystem>()).Forget();
        }
    }
}