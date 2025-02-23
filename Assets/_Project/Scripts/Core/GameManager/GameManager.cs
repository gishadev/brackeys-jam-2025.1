using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace BrackeysJam.Core
{
    public class GameManager : IGameManager, IInitializable, IDisposable
    {
        public async void Initialize()
        {
            await UniTask.WaitForSeconds(2f);
            StartGame();
        }

        public void Dispose()
        {
        }

        private void StartGame()
        {
            GameStarted?.Invoke();
        }

        public event Action GameStarted;
    }
}