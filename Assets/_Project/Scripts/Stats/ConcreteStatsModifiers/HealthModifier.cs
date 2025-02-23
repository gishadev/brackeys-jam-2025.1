using System;
using BrackeysJam.PlayerController;

namespace BrackeysJam._Project.Scripts.Stats.ConcreteStatsModifiers
{
    public class HealthModifier : StatModifier
    {
        public HealthModifier(IPlayerStats stats, ApplyType applyType, int value) : base(stats, applyType, value)
        {
        }

        public override void Handle()
        {
            switch (_applyType)
            {
                case ApplyType.Add:
                    _stats.CurrentHealth += _value;
                    break;
                case ApplyType.Multiply:
                    _stats.CurrentHealth *= _value;
                    break;
                case ApplyType.Percent:
                    _stats.CurrentHealth = _stats.CurrentHealth * _value / 100 + _stats.CurrentHealth;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}