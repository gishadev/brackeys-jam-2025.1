namespace BrackeysJam.PlayerController
{
    public interface IEnableable
    {
        public void Enable();
        public void Disable();
        bool IsEnabled { get; }
    }
}