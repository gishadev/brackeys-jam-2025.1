using BrackeysJam.EnemyController.SOs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BrackeysJam.EnemyController.Spawning
{
    [CreateAssetMenu(fileName = "EnemiesSpawningMasterDataSO", menuName = "ScriptableObjects/EnemiesSpawningMasterDataSO", order = 0)]
    public class EnemiesSpawningMasterDataSO : SerializedScriptableObject
    {
        [OdinSerialize, ShowInInspector, BoxGroup("General")]
        public int MaxEnemiesCount { private set; get; } = 100;

        [OdinSerialize, ShowInInspector, BoxGroup("General")]
        public int SpawningIterationDelayInSeconds { private set; get; } = 2;
        
        [SerializeField, ShowInInspector, BoxGroup("Groups")]
        private EnemyDataSO[] _enemiesData;

        [OdinSerialize, ShowInInspector, BoxGroup("Groups")]
        public int MinGroupSize { private set; get; } = 1;

        [OdinSerialize, ShowInInspector, BoxGroup("Groups")]
        public int MaxGroupSize { private set; get; } = 10;

        [OdinSerialize, ShowInInspector, BoxGroup("Difficulty Progression")]
        public int StartSpawnCash { private set; get; } = 200;
        [OdinSerialize, ShowInInspector, BoxGroup("Difficulty Progression"), InfoBox("Difficulty influences max difficulty of spawnable enemies.")]
        public int StartDifficultyLevel { private set; get; } = 200;
        
        [OdinSerialize, ShowInInspector, BoxGroup("Difficulty Progression")]
        public int MaxRoundTimeInSeconds { private set; get; } = 120;
        [OdinSerialize, ShowInInspector, BoxGroup("Difficulty Progression")]
        public int MaxEnemiesCountToMoveToNextRound { private set; get; } = 2;
        
        [OdinSerialize, ShowInInspector, BoxGroup("Difficulty Progression")]
        public int DifficultyLevelIncreasePerRound { private set; get; } = 1;
        [OdinSerialize, ShowInInspector, BoxGroup("Difficulty Progression")]
        public int SpawnCashIncreasePerRound { private set; get; } = 50;
        

        public EnemyDataSO[] EnemiesData => _enemiesData;
    }
}