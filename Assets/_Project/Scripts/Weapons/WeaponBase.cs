using System.Collections.Generic;
using System.Linq;
using BrackeysJam.Core;
using BrackeysJam.PlayerController;
using Cysharp.Threading.Tasks;
using gishadev.tools.Core;
using UnityEngine;

namespace BrackeysJam.Weapons
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        public WeaponDataSO WeaponData { get; private set; }

        protected List<ParticleSystem> _effectsPool;
        
        private IPlayerStats _playerStats;
        
        public virtual void Initialize(WeaponDataSO weaponData)
        {
            _effectsPool = new List<ParticleSystem>();

            WeaponData = weaponData;

            var effect = Instantiate(weaponData.Effect, transform);
            effect.transform.ResetTransform();
            effect.gameObject.SetActive(false);

            _effectsPool.Add(effect);
        }

        public virtual void Equip(IPlayerStats stats)
        {
            Debug.Log($"{WeaponData.Name} equipped");
            _playerStats = stats;
        }

        public virtual void Unequip()
        {
            Debug.Log($"{WeaponData.Name} unequipped");
        }

        public abstract void Use();
        public void Damage(IDamageable target)
        {
            if (target == null)
                return;

            int damage = Mathf.RoundToInt(WeaponData.SpellAmplifier *
                                          (GetPercent(_playerStats.PhysicalPower, WeaponData.PhysicalAdd) +
                                           GetPercent(_playerStats.MagicPower, WeaponData.MagicAdd))); 
            
            target.TakeDamage(damage);
        }

        private float GetPercent(int value, int percent)
        {
            return value * percent / 100f;
        }
        
        protected async UniTaskVoid DisableEffect(ParticleSystem effect)
        {
            await UniTask.WaitForSeconds(WeaponData.MaxEffectTime);
            effect.Stop();
            await UniTask.WaitUntil(() => !effect.IsAlive());
            effect.gameObject.SetActive(false);
        }
    }
}