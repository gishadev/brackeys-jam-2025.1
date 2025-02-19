using UnityEngine;

namespace BrackeysJam.Weapons.ConcreteWeapons
{
    public class MeleeWeapon : WeaponBase
    {
        public override void Use()
        {
            Debug.Log("Use sword");
        }
    }
}