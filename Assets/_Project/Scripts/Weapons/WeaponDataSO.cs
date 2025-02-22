using BrackeysJam._Project.Scripts.UI;
using Sirenix.OdinInspector;
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
        public int Attacks = 1;
        public int CastCooldown;

        public bool CanExpire = true;
        [ShowIf("CanExpire")] public float ExpirationTime;
        
        [Header("Visuals")]
        public GameObject Prefab;
        public Sprite Icon;

        public float MaxEffectTime;
        public ParticleSystem Effect;
    }
}