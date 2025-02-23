using BrackeysJam.EnemyController.Spawning;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BrackeysJam.Core.SOs
{
    [CreateAssetMenu(fileName = "GameMasterDataSO", menuName = "ScriptableObjects/GameMasterDataSO", order = 0)]
    public class GameMasterDataSO : SerializedScriptableObject
    {
        [field: SerializeField] public Material EnemyBaseMaterial { get; private set; }
        [field: SerializeField] public Material EnemyEliteOutlineMaterial { get; private set; }
        
        [OdinSerialize, ShowInInspector, InlineEditor]
        public EnemiesSpawningMasterDataSO EnemiesSpawningDataSO { private set; get; }
    }
}