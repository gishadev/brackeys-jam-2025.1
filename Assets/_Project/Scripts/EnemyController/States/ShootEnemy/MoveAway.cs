using System.Collections;
using BrackeysJam.PlayerController;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class MoveAway : IState
    {
        private readonly Enemy _shootEnemy;
        private readonly EnemyMovement _enemyMovement;
        private Transform _playerTrans;

        public MoveAway(Enemy shootEnemy, EnemyMovement enemyMovement)
        {
            _shootEnemy = shootEnemy;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _playerTrans = _shootEnemy.PlayerTrans;
            _enemyMovement.StartCoroutine(MoveAwayRoutine());
        }

        public void OnExit()
        {
            _enemyMovement.StopAllCoroutines();
            _enemyMovement.ChangeMoveSpeed(_shootEnemy.EnemyDataSO.MoveSpeed);
        }

        private IEnumerator MoveAwayRoutine()
        {
            while (true)
            {
                var moveAwayDirection = (_shootEnemy.transform.position - _playerTrans.transform.position).normalized;
                var fleePosition = _shootEnemy.transform.position + moveAwayDirection * 100f;
                _enemyMovement.SetDestination(fleePosition);
                yield return new WaitForSeconds(0.6f);
            }
        }
    }
}