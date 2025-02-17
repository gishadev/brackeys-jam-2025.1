using System.Collections;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class Flee : StateWithElapsedTime
    {
        private readonly FastEnemy _fastEnemy;
        private readonly EnemyMovement _enemyMovement;

        public Flee(FastEnemy fastEnemy, EnemyMovement enemyMovement)
        {
            _fastEnemy = fastEnemy;
            _enemyMovement = enemyMovement;
        }

        public override void Tick()
        {
        }

        public override void OnEnter()
        {
            SetStartTime();
            _fastEnemy.ResetDamaged();
            _enemyMovement.ChangeMoveSpeed(_fastEnemy.EnemyDataSO.MoveSpeed *
                                           _fastEnemy.FastData.FleeMoveSpeedMultiplier);
            _enemyMovement.StartCoroutine(FleeRoutine());
        }

        public override void OnExit()
        {
            _enemyMovement.StopAllCoroutines();
            _enemyMovement.ChangeMoveSpeed(_fastEnemy.EnemyDataSO.MoveSpeed);
        }

        private IEnumerator FleeRoutine()
        {
            while (true)
            {
                var fleeDirection = (_fastEnemy.transform.position - _fastEnemy.PlayerTrans.position).normalized;
                var fleePosition = _fastEnemy.transform.position + fleeDirection * 100f;
                _enemyMovement.SetDestination(fleePosition);
                yield return new WaitForSeconds(0.6f);
            }
        }
    }
}