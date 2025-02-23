using BrackeysJam.EnemyController.SOs;
using gishadev.tools.Effects;
using UnityEngine;

namespace BrackeysJam.EnemyController.Spawning
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IOtherEmitter _otherEmitter;

        public EnemyFactory(IOtherEmitter otherEmitter)
        {
            _otherEmitter = otherEmitter;
        }
        
        public void SpawnEnemy(EnemyDataSO enemyDataSO, Vector3 position, Quaternion rotation)
        {
            var emittedEnemy = _otherEmitter
                .EmitAt(enemyDataSO.PoolEnumType, position, rotation)
                .GetComponent<Enemy>();
            emittedEnemy.SetData(enemyDataSO);
        }
    }
}