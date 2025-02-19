using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace BrackeysJam.EnemyController.SOs
{
    [CreateAssetMenu(fileName = "FastEnemyData", menuName = "ScriptableObjects/Enemies/FastEnemyData")]
    public class FleeingEnemyDataSO : EnemyDataSO
    {
        [Title("Flee")]
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public int MinDamageToFlee { private set; get; } = 1;

        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public float FleeMoveSpeedMultiplier { private set; get; } = 1.2f;
        
        [VerticalGroup("Split/Right")]
        [BoxGroup("Split/Right/Special"), GUIColor("yellow")]
        [OdinSerialize, ShowInInspector]
        public float FleeTime { private set; get; } = 2f;
    }
}