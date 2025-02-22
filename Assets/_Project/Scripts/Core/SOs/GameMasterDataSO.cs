using UnityEngine;

namespace BrackeysJam.Core.SOs
{
    [CreateAssetMenu(fileName = "GameMasterDataSO", menuName = "ScriptableObjects/GameMasterDataSO", order = 0)]
    public class GameMasterDataSO : ScriptableObject
    {
        [field: SerializeField] public Material EnemyBaseMaterial { get; private set; }
        [field: SerializeField] public Material EnemyEliteOutlineMaterial { get; private set; }
    }
}