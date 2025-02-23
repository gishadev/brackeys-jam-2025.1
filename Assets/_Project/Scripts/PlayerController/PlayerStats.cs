using System;
using BrackeysJam.Core;
using UnityEngine;

namespace BrackeysJam.PlayerController
{
    public class PlayerStats : MonoBehaviour, IDamageable, IPlayerStats
    {
        public event Action<int> HealthChanged;

        [field:SerializeField] public int StartHealth { get; set; }
        [field:SerializeField] public int CurrentHealth { get; set; }

        [field:SerializeField] public int PhysicalPower { get; set; }
        [field:SerializeField] public int MagicPower { get; set; }
        
        [field:SerializeField] public int Dexterity { get; set; }
        [field:SerializeField] public int Speed { get; set; }
        
        [field:SerializeField] public int Range { get; set; }

        public void TakeDamage(int count)
        {
            CurrentHealth -= count;
            HealthChanged?.Invoke(CurrentHealth);
        }
    }
}