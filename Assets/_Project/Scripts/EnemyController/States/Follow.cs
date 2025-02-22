using System.Collections;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace BrackeysJam.EnemyController.States
{
    public class Follow : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovementController _enemyMovementController;

        public Follow(Enemy enemy, EnemyMovementController enemyMovementController)
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
            _enemyMovementController.StartCoroutine(FollowRoutine());
        }

        public void OnExit()
        {
            _enemyMovementController.StopAllCoroutines();
        }

        private IEnumerator FollowRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.6f);
                _enemyMovementController.SetDestination(_enemy.Player.transform.position);
            }
        }
    }
}