using System.Threading;
using Cysharp.Threading.Tasks;

namespace BrackeysJam.EnemyController.States
{
    public class MeleeAttack : StateWithElapsedTime
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovementController _enemyMovementController;
        private CancellationTokenSource _cts;

        public MeleeAttack(Enemy enemy, EnemyMovementController enemyMovementController)
        {
            _enemy = enemy;
            _enemyMovementController = enemyMovementController;
        }

        public override void Tick()
        {
        }

        public override void OnEnter()
        {
            _cts = new CancellationTokenSource();
            _enemyMovementController.Stop();

            MeleeAttackAsync();
        }

        public override void OnExit()
        {
            _cts.Cancel();
        }

        private async void MeleeAttackAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_enemy.EnemyDataSO.MeleeAttackDelay, cancellationToken: _cts.Token)
                    .SuppressCancellationThrow();
                if (_cts.IsCancellationRequested)
                    return;
                
                OnAttacked();
            }
        }
        
        private void OnAttacked()
        {
            _enemy.Player.TakeDamage(_enemy.EnemyDataSO.MeleeAttackDamage);
            _enemy.Player.PhysicsImpactEffector.Act(_enemy.transform.position, 30f);
        }
    }
}