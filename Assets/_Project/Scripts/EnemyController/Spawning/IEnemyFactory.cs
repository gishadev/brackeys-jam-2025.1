using BrackeysJam.EnemyController.SOs;
using UnityEngine;

namespace BrackeysJam.EnemyController.Spawning
{
    public interface IEnemyFactory
    {
        void SpawnEnemy(EnemyDataSO enemyDataSO, Vector3 position, Quaternion rotation);
    }
}