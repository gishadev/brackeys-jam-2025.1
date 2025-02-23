using System;
using BrackeysJam.PlayerController;

namespace BrackeysJam._Project.Scripts.Stats.ConcreteStatsModifiers
{
    public class PhysicalPowerModifier : StatModifier
    {
        public PhysicalPowerModifier(IPlayerStats stats, ApplyType applyType, int value) : base(stats, applyType, value)
        {
        }

        public override void Handle()
        {
            switch (_applyType)
            {
                case ApplyType.Add:
                    _stats.PhysicalPower += _value;
                    break;
                case ApplyType.Multiply:
                    _stats.PhysicalPower *= _value;
                    break;
                case ApplyType.Percent:
                    _stats.PhysicalPower = _stats.PhysicalPower * _value / 100 + _stats.PhysicalPower;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}