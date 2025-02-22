using System.Collections;
using BrackeysJam.PlayerController;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class MoveAway : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _enemyMovement;

        public MoveAway(Enemy enemy, EnemyMovement enemyMovement)
        {
            _enemy = enemy;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovement.StartCoroutine(MoveAwayRoutine());
        }

        public void OnExit()
        {
            _enemyMovement.StopAllCoroutines();
            _enemyMovement.ChangeMoveSpeed(_enemy.EnemyDataSO.MoveSpeed);
        }

        private IEnumerator MoveAwayRoutine()
        {
            while (true)
            {
                var moveAwayDirection = (_enemy.transform.position - _enemy.Player.transform.position).normalized;
                var fleePosition = _enemy.transform.position + moveAwayDirection * 100f;
                _enemyMovement.SetDestination(fleePosition);
                yield return new WaitForSeconds(0.6f);
            }
        }
    }
}