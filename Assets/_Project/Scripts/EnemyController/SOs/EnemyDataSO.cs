using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace BrackeysJam.EnemyController.SOs
{
    public abstract class EnemyDataSO : SerializedScriptableObject
    {
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        [BoxGroup("Split/Left/General")]
        [ValidateInput(nameof(CanOnlyBeEnemyEnum), "For ENEMY ENUM only")]
        [OdinSerialize, ShowInInspector, InfoBox("Select only ENEMY enum!")]
        public OtherPoolEnum PoolEnumType { private set; get; }

        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector]
        public bool IsElite { private set; get; }
        
        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector, GUIColor("green")]
        public int StartHealth { private set; get; } = 2;

        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector]
        public float MoveSpeed { private set; get; } = 100f;
        
        [BoxGroup("Split/Left/General")]
        [OdinSerialize, ShowInInspector]
        public float StartAreaRadius { private set; get; } = 1.5f;
        
        [BoxGroup("Split/Left/General"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public float MeleeAttackRadius { private set; get; } = 1f;

        [BoxGroup("Split/Left/General"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public float MeleeAttackDelay { private set; get; } = 0.5f;

        [BoxGroup("Split/Left/General"), GUIColor("red")]
        [OdinSerialize, ShowInInspector]
        public int MeleeAttackDamage { private set; get; } = 2;
        
        [BoxGroup("Split/Left/General"), GUIColor("blue")]
        [OdinSerialize, ShowInInspector]
        public float FollowRadius { private set; get; } = 10f;

        [BoxGroup("Spawning")]
        [OdinSerialize, ShowInInspector]
        public int SpawningPrice { private set; get; } = 10;
        
        [BoxGroup("Spawning")]
        [OdinSerialize, ShowInInspector]
        public int SpawningDifficultyLevel { private set; get; } = 1;
        
        private bool CanOnlyBeEnemyEnum(OtherPoolEnum poolEnum, ref string errorMessage)
        {
            if (poolEnum.ToString().Contains("ENEMY"))
                return true;
            return false;
        }
    }
}