using BrackeysJam.Core.SOs;
using BrackeysJam.PlayerController;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class EnemyMaterialController : IEnableable
    {
        private GameMasterDataSO _gameMasterDataSO;
        private SpriteRenderer _spriteRenderer;
        public bool IsEnabled { get; private set; }

        public EnemyMaterialController(SpriteRenderer spriteRenderer, GameMasterDataSO gameMasterDataSO)
        {
            _spriteRenderer = spriteRenderer;
            _gameMasterDataSO = gameMasterDataSO;
            Enable();
        }
        
        public void Enable() => IsEnabled = true;
        public void Disable() => IsEnabled = false;

        public void SetOutlineMaterial() => _spriteRenderer.material = _gameMasterDataSO.EnemyEliteOutlineMaterial;
        public void SetDefaultMaterial() => _spriteRenderer.material = _gameMasterDataSO.EnemyBaseMaterial;
    }
}