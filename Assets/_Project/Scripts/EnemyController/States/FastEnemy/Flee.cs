using System.Collections;
using UnityEngine;

namespace BrackeysJam.EnemyController.States.FastEnemy
{
    public class Flee : StateWithElapsedTime
    {
        private readonly EnemyController.FleeingEnemy _fleeingEnemy;
        private readonly EnemyMovement _enemyMovement;

        public Flee(EnemyController.FleeingEnemy fleeingEnemy, EnemyMovement enemyMovement)
        {
            _fleeingEnemy = fleeingEnemy;
            _enemyMovement = enemyMovement;
        }

        public override void Tick()
        {
        }

        public override void OnEnter()
        {
            SetStartTime();
            _fleeingEnemy.ResetDamaged();
            _enemyMovement.ChangeMoveSpeed(_fleeingEnemy.EnemyDataSO.MoveSpeed *
                                           _fleeingEnemy.FleeingData.FleeMoveSpeedMultiplier);
            _enemyMovement.StartCoroutine(FleeRoutine());
        }

        public override void OnExit()
        {
            _enemyMovement.StopAllCoroutines();
            _enemyMovement.ChangeMoveSpeed(_fleeingEnemy.EnemyDataSO.MoveSpeed);
        }

        private IEnumerator FleeRoutine()
        {
            while (true)
            {
                var fleeDirection = (_fleeingEnemy.transform.position - _fleeingEnemy.Player.transform.position).normalized;
                var fleePosition = _fleeingEnemy.transform.position + fleeDirection * 100f;
                _enemyMovement.SetDestination(fleePosition);
                yield return new WaitForSeconds(0.6f);
            }
        }
    }
}