using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using gishadev.tools.Core;
using UnityEngine;

namespace BrackeysJam.Weapons
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        public WeaponDataSO WeaponData { get; private set; }

        protected List<ParticleSystem> _effectsPool;
        
        public virtual void Initialize(WeaponDataSO weaponData)
        {
            _effectsPool = new List<ParticleSystem>();

            WeaponData = weaponData;

            var effect = Instantiate(weaponData.Effect, transform);
            effect.transform.ResetTransform();
            effect.gameObject.SetActive(false);

            _effectsPool.Add(effect);
        }

        public virtual void Equip()
        {
            Debug.Log($"{WeaponData.Name} equipped");
        }

        public virtual void Unequip()
        {
            Debug.Log($"{WeaponData.Name} unequipped");
        }

        public abstract void Use();

        protected async UniTaskVoid DisableEffect(ParticleSystem effect)
        {
            await UniTask.WaitForSeconds(WeaponData.MaxEffectTime);
            effect.Stop();
            await UniTask.WaitUntil(() => !effect.IsAlive());
            effect.gameObject.SetActive(false);
        }
    }
}