namespace BrackeysJam.Weapons
{
    public interface IWeapon
    {
        public WeaponDataSO WeaponData { get; }
        
        public void Initialize(WeaponDataSO weaponData);
        public void Equip();
        public void Unequip();
        public void Use();
    }
}