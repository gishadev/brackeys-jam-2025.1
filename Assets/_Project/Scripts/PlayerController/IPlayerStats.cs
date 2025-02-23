namespace BrackeysJam.PlayerController
{
    public interface IPlayerStats
    {
        public int StartHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int PhysicalPower { get; set; }
        public int MagicPower { get; set; }
        public int Dexterity { get; set; }
        public int Speed { get; set; }
        public int Range { get; set; }
    }
}