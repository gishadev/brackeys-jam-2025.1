using UnityEngine;

namespace BrackeysJam.Weapons.Factory
{
    public class WeaponFactory : IWeaponFactory
    {
        public GameObject CreateWeapon(WeaponDataSO config)
        {
            if (config == null || config.Prefab == null)
                throw new System.ArgumentNullException("Invalid config");
            
            var weaponInstance = Object.Instantiate(config.Prefab);
            var weaponComponent = weaponInstance.GetComponent<IWeapon>();
            weaponComponent.Initialize(config);

            return weaponInstance;
        }
    }
}