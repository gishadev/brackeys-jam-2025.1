namespace BrackeysJam.PlayerController.InputReaders
{
    public interface IInputReader<T>
    {
        public T Read();
    }
}