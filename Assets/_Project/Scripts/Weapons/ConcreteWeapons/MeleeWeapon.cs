using System.Linq;
using gishadev.tools.Core;
using UnityEngine;

namespace BrackeysJam.Weapons.ConcreteWeapons
{
    public class MeleeWeapon : WeaponBase
    {
        public override void Use()
        {
            PlayEffect();
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