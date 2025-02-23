using BrackeysJam.Core;
using BrackeysJam.EnemyController.Spawning;
using Zenject;

namespace BrackeysJam.Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemiesSpawningSystem>().AsSingle().NonLazy();
        }
    }
}