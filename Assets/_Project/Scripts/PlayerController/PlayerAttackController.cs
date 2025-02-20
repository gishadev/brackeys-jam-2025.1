using System.Collections.Generic;
using System.Linq;
using BrackeysJam.UI.WeaponUI;
using BrackeysJam.Weapons;
using BrackeysJam.Weapons.Factory;
using gishadev.tools.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.PlayerController
{
    public class PlayerAttackController : MonoBehaviour, IEnableable
    {
        [SerializeField, Required] private WeaponDataSO _baseWeapon;
        
        [Zenject.Inject] private IWeaponUIContainer _weaponUIContainer;

        private IWeaponFactory _weaponFactory;
        
        private List<IWeapon> _activeWeapons;
        
        private Dictionary<IWeapon, float> _weaponToExpirationTime;
        private Dictionary<IWeapon, float> _weaponToCastTime;
        
        private bool _enabled = false;

        private void Update()
        {
            UpdateExpiration();
            UpdateCastTime();
        }

        public void Initialize()
        {
            _weaponFactory = new WeaponFactory();
            _activeWeapons = new List<IWeapon>();
            _weaponToExpirationTime = new Dictionary<IWeapon, float>();
            _weaponToCastTime = new Dictionary<IWeapon, float>();
            
            LoadWeapon(_baseWeapon);

            Enable();
        }

        private void LoadWeapon(WeaponDataSO weaponData)
        {
            var weaponGO = _weaponFactory.CreateWeapon(weaponData);
            var weapon = weaponGO.GetComponent<IWeapon>();
            
            weaponGO.transform.parent = transform;
            weaponGO.transform.ResetTransform();

            _activeWeapons.Add(weapon);
            if (weaponData.CanExpire)
                _weaponToExpirationTime.Add(weapon, weaponData.ExpirationTime);
            _weaponToCastTime.Add(weapon, 0);

            _weaponUIContainer.AddWeapon(weaponData);

            weapon.Equip();
        }

        private void RemoveWeapon(IWeapon weapon)
        {
            _activeWeapons.Remove(weapon);
            _weaponToExpirationTime.Remove(weapon);
            _weaponToCastTime.Remove(weapon);

            _weaponUIContainer.RemoveWeapon(weapon.WeaponData);
            
            weapon.Unequip();
        }
        
        private void UpdateExpiration()
        {
            foreach (var weapon in _weaponToExpirationTime.Keys.ToList())
            {
                if (_weaponToExpirationTime[weapon] <= 0)
                    RemoveWeapon(weapon);
                else
                    _weaponToExpirationTime[weapon] -= Time.deltaTime;
            }
        }

        private void UpdateCastTime()
        {
            foreach (var weapon in _weaponToCastTime.Keys.ToList())
            {
                if (_weaponToCastTime[weapon] <= 0)
                {
                    weapon.Use();
                    _weaponToCastTime[weapon] = weapon.WeaponData.CastCooldown;
                }
                else
                    _weaponToCastTime[weapon] -= Time.deltaTime;
            }
        }
        
        #region IEnableable

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        #endregion
    }
}