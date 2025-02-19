using System.Threading;
using BrackeysJam.EnemyController;
using Cysharp.Threading.Tasks;
using gishadev.tools.Audio;
using gishadev.tools.Effects;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    public class Shooting : IState
    {
        private readonly ShootEnemy _shootEnemy;
        private readonly EnemyMovement _enemyMovement;
        private Transform _playerTrans;
        private CancellationTokenSource _cts;

        public Shooting(ShootEnemy shootEnemy, EnemyMovement enemyMovement)
        {
            _shootEnemy = shootEnemy;
            _enemyMovement = enemyMovement;
        }

        public void Tick()
        {
            _enemyMovement.FlipTowardsPosition(_playerTrans.transform.position);
        }

        public void OnEnter()
        {
            _enemyMovement.Stop();
            _cts = new CancellationTokenSource();
            _playerTrans = _shootEnemy.PlayerTrans;
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
                await UniTask.WaitForSeconds(_shootEnemy.ShootData.ShootDelay, cancellationToken: _cts.Token)
                    .SuppressCancellationThrow();
                if (_cts.IsCancellationRequested)
                    return;

                // var shootDir = (_playerTrans.transform.position - _shootEnemy.transform.position).normalized;
                // var shootRotation =
                //     Quaternion.AngleAxis(Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg, Vector3.forward);

                // OtherPoolEnum projectilePoolKey = _shootEnemy.EnemyDataSO.IsBoss
                //     ? OtherPoolEnum.BOSS_PROJECTILE
                //     : OtherPoolEnum.ENEMY_PROJECTILE;
                //
                // var projectile = OtherEmitter.I
                //     .EmitAt(projectilePoolKey, _shootEnemy.ShootPoint.position, shootRotation)
                //     .GetComponent<EnemyProjectile>();
                // projectile.SetDamage(_shootEnemy.ShootData.ShootProjectileDamage);
                
                // AudioManager.I.PlayAudio(SFXAudioEnum.ENEMY_SHOOT);
            }
        }
    }
}