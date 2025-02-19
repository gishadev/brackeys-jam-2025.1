using System.Collections.Generic;
using System.Threading.Tasks;
using BrackeysJam.UI.WeaponUI;
using BrackeysJam.Weapons;
using BrackeysJam.Weapons.Factory;
using Cysharp.Threading.Tasks;
using gishadev.tools.Core;
using UnityEngine;

namespace BrackeysJam.PlayerController
{
    public class PlayerAttackController : MonoBehaviour, IEnableable
    {
        [Zenject.Inject] private IWeaponUIContainer _weaponUIContainer;

        private IWeaponFactory _weaponFactory;
        
        private List<IWeapon> _activeWeapons;
        
        private bool _enabled = false;
        
        public async UniTask Initialize()
        {
            _weaponFactory = new WeaponFactory();
            _activeWeapons = new List<IWeapon>();
            
            await LoadWeapon("SwordData");

            Enable();
        }

        private async UniTask LoadWeapon(string assetPath)
        {
            var request = Resources.LoadAsync(assetPath).ToUniTask();
            var result = await request;

            var weapon = _weaponFactory.CreateWeapon(result as WeaponDataSO);

            weapon.transform.parent = transform;
            weapon.transform.ResetTransform();
            
            _activeWeapons.Add(weapon.GetComponent<IWeapon>());
            _weaponUIContainer.AddWeapon(result as WeaponDataSO);
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