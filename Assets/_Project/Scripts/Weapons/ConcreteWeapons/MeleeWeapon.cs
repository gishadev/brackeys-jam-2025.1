using System.Linq;
using BrackeysJam.Core;
using gishadev.tools.Core;
using UnityEngine;

namespace BrackeysJam.Weapons.ConcreteWeapons
{
    public class MeleeWeapon : WeaponBase
    {
        private Collider2D[] _colliders = new Collider2D[5];
        
        public override void Use()
        {
            PlayEffect();
            GetTarget();
            foreach (var col in _colliders)
            {
                if (col == null)
                    continue;
                Damage(col.GetComponent<IDamageable>());
            }
        }
        
        private void GetTarget()
        {
            for (int i = 0; i < _colliders.Length; i++)
                _colliders[i] = null;

            Physics2D.OverlapCircleNonAlloc(transform.position, WeaponData.Range, _colliders, LayerMask.GetMask("Enemy"));
        }
        
        private void PlayEffect()
        {
            var freeEffect = _effectsPool.FirstOrDefault(e => !e.gameObject.activeSelf);
            if (freeEffect == null)
            {
                freeEffect = Instantiate(WeaponData.Effect, transform);
                freeEffect.transform.ResetTransform();
                freeEffect.gameObject.SetActive(false);

                _effectsPool.Add(freeEffect);
            }
            
            freeEffect.gameObject.SetActive(true);
            freeEffect.Play();
            DisableEffect(freeEffect).Forget();
        }
    }
}