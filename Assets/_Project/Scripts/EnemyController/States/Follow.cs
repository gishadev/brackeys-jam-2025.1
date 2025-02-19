using System.Collections;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace BrackeysJam.EnemyController.States
{
    public class Follow : IState
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _enemyMovement;

        public Follow(Enemy enemy, EnemyMovement enemyMovement)
        {
            _enemy = enemy;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _enemyMovement.ChangeMoveSpeed(_enemy.EnemyDataSO.MoveSpeed);
            _enemyMovement.StartCoroutine(FollowRoutine());
        }

        public void OnExit()
        {
            _enemyMovement.StopAllCoroutines();
        }

        private IEnumerator FollowRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.6f);
                _enemyMovement.SetDestination(_enemy.PlayerTrans.position);
            }
        }
    }
}