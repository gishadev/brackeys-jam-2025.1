using System;
using BrackeysJam.PlayerController;

namespace BrackeysJam._Project.Scripts.Stats.ConcreteStatsModifiers
{
    public class DexterityModifier : StatModifier
    {
        public DexterityModifier(IPlayerStats stats, ApplyType applyType, int value) : base(stats, applyType, value)
        {
        }

        public override void Handle()
        {
            switch (_applyType)
            {
                case ApplyType.Add:
                    _stats.Dexterity += _value;
                    break;
                case ApplyType.Multiply:
                    _stats.Dexterity *= _value;
                    break;
                case ApplyType.Percent:
                    _stats.Dexterity = _stats.Dexterity * _value / 100 + _stats.Dexterity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}