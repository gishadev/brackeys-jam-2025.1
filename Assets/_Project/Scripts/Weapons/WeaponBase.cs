using UnityEngine;

namespace BrackeysJam.Weapons
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        public WeaponDataSO Data { get; private set; }
        
        public virtual void Initialize(WeaponDataSO weaponData)
        {
            Data = weaponData;
        }

        public virtual void Equip()
        {
            Debug.Log($"{Data.Name} equipped");
        }

        public virtual void Unequip()
        {
            Debug.Log($"{Data.Name} unequipped");
        }

        public abstract void Use();
    }
}