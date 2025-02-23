using System;
using BrackeysJam.PlayerController;

namespace BrackeysJam._Project.Scripts.Stats.ConcreteStatsModifiers
{
    public class RangeModifier : StatModifier
    {
        public RangeModifier(IPlayerStats stats, ApplyType applyType, int value) : base(stats, applyType, value)
        {
        }

        public override void Handle()
        {
            switch (_applyType)
            {
                case ApplyType.Add:
                    _stats.Range += _value;
                    break;
                case ApplyType.Multiply:
                    _stats.Range *= _value;
                    break;
                case ApplyType.Percent:
                    _stats.Range = _stats.Range * _value / 100 + _stats.Range;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}