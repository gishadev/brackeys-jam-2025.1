using BrackeysJam.Core.SOs;
using UnityEngine;
using Zenject;

namespace BrackeysJam.Infrastructure
{
    [CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
    public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
    {
        [SerializeField] private GameMasterDataSO _gameMasterDataSO;

        public override void InstallBindings()
        {
            Container.BindInstances(_gameMasterDataSO);
        }
    }
}