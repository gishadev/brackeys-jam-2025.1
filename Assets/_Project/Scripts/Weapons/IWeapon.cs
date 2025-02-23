using BrackeysJam.Core;
using BrackeysJam.PlayerController;

namespace BrackeysJam.Weapons
{
    public interface IWeapon
    {
        public WeaponDataSO WeaponData { get; }
        
        public void Initialize(WeaponDataSO weaponData);
        public void Equip(IPlayerStats stats);
        public void Unequip();
        public void Use();
        public void Damage(IDamageable target);
    }
}