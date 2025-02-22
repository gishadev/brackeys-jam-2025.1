using BrackeysJam.Core;
using BrackeysJam.EnemyController;
using UnityEngine;

namespace BrackeysJam.Projectiles
{
    public class EnemyProjectile : Projectile
    {
        private int _damageCount = 1;

        public void SetDamage(int damageCount)
        {
            _damageCount = damageCount;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.TryGetComponent(out IDamageable damageable))
            {
                if (damageable is Enemy)
                    return;

                damageable.TakeDamage(_damageCount);
            }

            Die();
        }
    }
}