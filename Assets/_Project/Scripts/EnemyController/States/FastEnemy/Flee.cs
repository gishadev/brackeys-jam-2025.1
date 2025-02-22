using System.Collections;
using UnityEngine;

namespace BrackeysJam.EnemyController.States.FastEnemy
{
    public class Flee : StateWithElapsedTime
    {
        private readonly EnemyController.FleeingEnemy _fleeingEnemy;
        private readonly EnemyMovementController _enemyMovementController;

        public Flee(EnemyController.FleeingEnemy fleeingEnemy, EnemyMovementController enemyMovementController)
        {
            _fleeingEnemy = fleeingEnemy;
            _enemyMovementController = enemyMovementController;
        }

        public override void Tick()
        {
        }

        public override void OnEnter()
        {
            SetStartTime();
            _fleeingEnemy.ResetDamaged();
            _enemyMovementController.ChangeMoveSpeed(_fleeingEnemy.EnemyDataSO.MoveSpeed *
                                           _fleeingEnemy.FleeingData.FleeMoveSpeedMultiplier);
            _enemyMovementController.StartCoroutine(FleeRoutine());
        }

        public override void OnExit()
        {
            _enemyMovementController.StopAllCoroutines();
            _enemyMovementController.ChangeMoveSpeed(_fleeingEnemy.EnemyDataSO.MoveSpeed);
        }

        private IEnumerator FleeRoutine()
        {
            while (true)
            {
                var fleeDirection = (_fleeingEnemy.transform.position - _fleeingEnemy.Player.transform.position).normalized;
                var fleePosition = _fleeingEnemy.transform.position + fleeDirection * 100f;
                _enemyMovementController.SetDestination(fleePosition);
                yield return new WaitForSeconds(0.6f);
            }
        }
    }
}