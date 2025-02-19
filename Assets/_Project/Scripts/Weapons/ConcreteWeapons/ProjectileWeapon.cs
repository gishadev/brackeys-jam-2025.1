using UnityEngine;

namespace BrackeysJam.Weapons.ConcreteWeapons
{
    public class ProjectileWeapon : WeaponBase
    {
        public override void Use()
        {
            Debug.Log("Use projectile");
        }
    }
}