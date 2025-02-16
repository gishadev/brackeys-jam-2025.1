using gishadev.tools.Audio;
using gishadev.tools.Effects;
using gishadev.tools.SceneLoading;
using Zenject;

public class GishadevToolsMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<AudioManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<SFXEmitter>().AsSingle().NonLazy();
        Container.BindInterfacesTo<VFXEmitter>().AsSingle().NonLazy();
        Container.BindInterfacesTo<OtherEmitter>().AsSingle().NonLazy();
        Container.BindInterfacesTo<SceneLoader>().AsSingle().NonLazy();
    }
}