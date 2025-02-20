using System;
using UnityEngine;

namespace BrackeysJam.Core
{
    public interface IDamageable
    {
        int StartHealth { get; }
        int CurrentHealth { get; }
        event Action<int> HealthChanged;
        void TakeDamage(int count);

        GameObject gameObject { get; }
        Transform transform { get; }
    }
}