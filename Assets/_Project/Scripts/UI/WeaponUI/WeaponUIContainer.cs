using BrackeysJam.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BrackeysJam.UI.WeaponUI
{
    public class WeaponUIContainer : MonoBehaviour, IWeaponUIContainer
    {
        [SerializeField, Required] private Transform _container;
        [SerializeField, Required] private WeaponUiIcon _iconPrefab;
        
        public void AddWeapon(WeaponDataSO weaponData)
        {
            // todo next step is to create icon
            throw new System.NotImplementedException();
        }
    }
}