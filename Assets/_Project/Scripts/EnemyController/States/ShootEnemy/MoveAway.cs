using System.Collections;
using BrackeysJam.PlayerController;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class MoveAway : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovementController _enemyMovementController;

        public MoveAway(Enemy enemy, EnemyMovementController enemyMovementController)
        {
            _enemy = enemy;
            _enemyMovementController = enemyMovementController;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovementController.StartCoroutine(MoveAwayRoutine());
        }

        public void OnExit()
        {
            _enemyMovementController.StopAllCoroutines();
            _enemyMovementController.ChangeMoveSpeed(_enemy.EnemyDataSO.MoveSpeed);
        }

        private IEnumerator MoveAwayRoutine()
        {
            while (true)
            {
                var moveAwayDirection = (_enemy.transform.position - _enemy.Player.transform.position).normalized;
                var fleePosition = _enemy.transform.position + moveAwayDirection * 100f;
                _enemyMovementController.SetDestination(fleePosition);
                yield return new WaitForSeconds(0.6f);
            }
        }
    }
}