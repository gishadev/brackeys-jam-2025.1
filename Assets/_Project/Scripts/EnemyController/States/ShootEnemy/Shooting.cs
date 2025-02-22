using System.Threading;
using BrackeysJam.Projectiles;
using Cysharp.Threading.Tasks;
using gishadev.tools.Effects;
using gishadev.tools.StateMachine;
using UnityEngine;
using Zenject;

namespace BrackeysJam.EnemyController
{
    public class Shooting : IState
    {
        [Inject] private IOtherEmitter _otherEmitter;

        private readonly ShootEnemy _enemy;
        private readonly EnemyMovementController _enemyMovementController;
        private CancellationTokenSource _cts;

        public Shooting(ShootEnemy enemy, EnemyMovementController enemyMovementController, DiContainer diContainer)
        {
            diContainer.Inject(this);
            _enemy = enemy;
            _enemyMovementController = enemyMovementController;
        }

        public void Tick()
        {
            _enemyMovementController.FlipTowardsPosition(_enemy.Player.transform.position);
        }

        public void OnEnter()
        {
            _enemyMovementController.Stop();
            _cts = new CancellationTokenSource();
            ShootAsync();
        }

        public void OnExit()
        {
            _cts.Cancel();
        }

        private async void ShootAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_enemy.ShootData.ShootDelay, cancellationToken: _cts.Token)
                    .SuppressCancellationThrow();
                if (_cts.IsCancellationRequested)
                    return;

                var shootDir = (_enemy.Player.transform.position - _enemy.transform.position).normalized;
                var shootRotation =
                    Quaternion.AngleAxis(Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg, Vector3.forward);

                OtherPoolEnum projectilePoolKey = OtherPoolEnum.ENEMY_PROJECTILE;
                var projectile = _otherEmitter
                    .EmitAt(projectilePoolKey, _enemy.ShootPoint.position, shootRotation)
                    .GetComponent<EnemyProjectile>();
                projectile.SetDamage(_enemy.ShootData.ShootProjectileDamage);

                // AudioManager.I.PlayAudio(SFXAudioEnum.ENEMY_SHOOT);
            }
        }
    }
}