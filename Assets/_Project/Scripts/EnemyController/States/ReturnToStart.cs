using System.Collections;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace BrackeysJam.EnemyController.States
{
    public class ReturnToStart : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovementController _enemyMovementController;

        public ReturnToStart(Enemy enemy, EnemyMovementController enemyMovementController)
        {
            _enemy = enemy;
            _enemyMovementController = enemyMovementController;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovementController.ChangeMoveSpeed(_enemy.EnemyDataSO.MoveSpeed);
            _enemyMovementController.StartCoroutine(ReturnRoutine());
        }

        public void OnExit()
        {
            _enemyMovementController.StopAllCoroutines();
        }

        private IEnumerator ReturnRoutine()
        {
            while (true)
            {
                _enemyMovementController.SetDestination(_enemy.StartPosition);
                yield return new WaitForSeconds(2f);
            }
        }
    }
}