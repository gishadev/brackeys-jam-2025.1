using System.Collections.Generic;
using System.Linq;
using BrackeysJam.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.UI.WeaponUI
{
    public class WeaponUIContainer : MonoBehaviour, IWeaponUIContainer
    {
        [SerializeField, Required] private Transform _container;
        [SerializeField, Required] private WeaponUiIcon _iconPrefab;

        private List<WeaponUiIcon> _activeWeaponIcons;
        
        public void AddWeapon(WeaponDataSO weaponData)
        {
            _activeWeaponIcons ??= new List<WeaponUiIcon>();

            var icon = _activeWeaponIcons.FirstOrDefault(i => !i.gameObject.activeSelf) ?? Instantiate(_iconPrefab, _container);
            icon.SetIconParams(weaponData);

            _activeWeaponIcons.Add(icon);
        }

        public void RemoveWeapon(WeaponDataSO weaponData)
        {
            _activeWeaponIcons.FirstOrDefault(i => i.StoredData == weaponData)?.gameObject.SetActive(false);
        }
    }
}