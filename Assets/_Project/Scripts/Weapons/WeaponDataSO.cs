using BrackeysJam._Project.Scripts.UI;
using UnityEngine;

namespace BrackeysJam.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Config/WeaponData")]
    public class WeaponDataSO : ScriptableObject, IIconParams
    {
        [Header("General Info")]
        public string Name;
        
        [Header("Damage Info")]
        public int Damage;
        public int Range;
        public int Cooldown;
        
        [Header("Visuals")]
        public GameObject Prefab;
        public Sprite Icon;
    }
}