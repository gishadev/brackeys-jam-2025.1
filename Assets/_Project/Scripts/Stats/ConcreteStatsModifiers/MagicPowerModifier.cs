using System;
using BrackeysJam.PlayerController;

namespace BrackeysJam._Project.Scripts.Stats.ConcreteStatsModifiers
{
    public class MagicPowerModifier : StatModifier
    {
        public MagicPowerModifier(IPlayerStats stats, ApplyType applyType, int value) : base(stats, applyType, value)
        {
        }

        public override void Handle()
        {
            switch (_applyType)
            {
                case ApplyType.Add:
                    _stats.MagicPower += _value;
                    break;
                case ApplyType.Multiply:
                    _stats.MagicPower *= _value;
                    break;
                case ApplyType.Percent:
                    _stats.MagicPower = _stats.MagicPower * _value / 100 + _stats.MagicPower;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}