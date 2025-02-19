namespace BrackeysJam.Weapons
{
    public interface IWeapon
    {
        public void Initialize(WeaponDataSO weaponData);
        public void Equip();
        public void Unequip();
        public void Use();
    }
}