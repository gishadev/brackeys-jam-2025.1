using BrackeysJam.Weapons;

namespace BrackeysJam.UI.WeaponUI
{
    public interface IWeaponUIContainer
    {
        public void AddWeapon(WeaponDataSO weaponData);
        public void RemoveWeapon(WeaponDataSO weaponData);
    }
}