using BrackeysJam.PlayerController;

namespace BrackeysJam._Project.Scripts.Stats
{
    public abstract class StatModifier : IStatModifier
    {
        public enum ApplyType
        {
            Add,
            Multiply,
            Percent,
        }
        
        protected readonly IPlayerStats _stats;
        protected readonly int _value;
        protected readonly ApplyType _applyType;

        public StatModifier(IPlayerStats stats, ApplyType applyType, int value)
        {
            _stats = stats;
            _applyType = applyType;
            _value = value;
        }

        public abstract void Handle();
    }
}