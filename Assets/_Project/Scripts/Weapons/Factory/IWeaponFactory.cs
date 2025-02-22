using UnityEngine;

namespace BrackeysJam.Weapons.Factory
{
    public interface IWeaponFactory
    {
        public GameObject CreateWeapon(WeaponDataSO config);
    }
}