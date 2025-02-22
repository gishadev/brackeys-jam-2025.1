using BrackeysJam.UI.WeaponUI;
using UnityEngine;
using Zenject;

namespace BrackeysJam._Project.Scripts.MonoInstallers
{
    public class WeaponUIContainerInstaller : MonoInstaller
    {
        [SerializeField] private WeaponUIContainer _weaponUIContainer;

        public override void InstallBindings()
        {
            Container.Bind<IWeaponUIContainer>().To<WeaponUIContainer>().FromInstance(_weaponUIContainer).AsSingle();
        }
    }
}