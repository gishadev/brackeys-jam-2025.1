using System;
using BrackeysJam.PlayerController;

namespace BrackeysJam._Project.Scripts.Stats.ConcreteStatsModifiers
{
    public class SpeedModifier : StatModifier
    {
        public SpeedModifier(IPlayerStats stats, ApplyType applyType, int value) : base(stats, applyType, value)
        {
        }

        public override void Handle()
        {
            switch (_applyType)
            {
                case ApplyType.Add:
                    _stats.Speed += _value;
                    break;
                case ApplyType.Multiply:
                    _stats.Speed *= _value;
                    break;
                case ApplyType.Percent:
                    _stats.Speed = _stats.Speed * _value / 100 + _stats.Speed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}