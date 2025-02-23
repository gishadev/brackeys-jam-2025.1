using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BrackeysJam.Core;
using BrackeysJam.Core.SOs;
using BrackeysJam.EnemyController.SOs;
using Cysharp.Threading.Tasks;
using gishadev.tools.Effects;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BrackeysJam.EnemyController.Spawning
{
    public class EnemiesSpawningSystem : IInitializable, IDisposable, IEnemiesSpawningSystem
    {
        [Inject] private IGameManager _gameManager;
        [Inject] private GameMasterDataSO _gameMasterDataSO;
        [Inject] private IOtherEmitter _otherEmitter;

        private EnemiesSpawningMasterDataSO SpawningData => _gameMasterDataSO.EnemiesSpawningDataSO;
        private IEnemyFactory _enemyFactory;
        private ISpawningPositionGetter _positionGetter;

        private int _currentSpawnCash;
        private int _currentSpawnMaxDifficulty;

        private int _currentStartSpawnCash;
        private CancellationTokenSource _spawningSystemCts;

        public void Initialize()
        {
            _gameManager.GameStarted += OnGameStarted;

            _enemyFactory = new EnemyFactory(_otherEmitter);
            _positionGetter = new RectangleBoundsSpawningPositionGetter(Vector2.zero, Vector2.one * 10f);

            _spawningSystemCts = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _gameManager.GameStarted -= OnGameStarted;
            _spawningSystemCts?.Cancel();
        }

        private void OnGameStarted()
        {
            _currentStartSpawnCash = SpawningData.StartSpawnCash;
            _currentSpawnCash = _currentStartSpawnCash;

            _currentSpawnMaxDifficulty = SpawningData.StartDifficultyLevel;
            SpawningRoundAsync();
        }

        private async void SpawningRoundAsync()
        {
            while (!_spawningSystemCts.IsCancellationRequested)
            {
                var spawningCts = CancellationTokenSource.CreateLinkedTokenSource(_spawningSystemCts.Token);
                SpawningAsync(spawningCts.Token);

                var timerAsync = UniTask.WaitForSeconds(SpawningData.MaxRoundTimeInSeconds,
                    cancellationToken: _spawningSystemCts.Token).SuppressCancellationThrow();
                var enemiesCountAsync = UniTask.WaitUntil(() =>
                        GameObject.FindGameObjectsWithTag(Constants.ENEMY_TAG_NAME).Length == 0,
                    cancellationToken: _spawningSystemCts.Token).SuppressCancellationThrow();

                await UniTask.WhenAny(timerAsync, enemiesCountAsync);

                spawningCts.Cancel();
                _currentStartSpawnCash += SpawningData.SpawnCashIncreasePerRound;
                _currentSpawnMaxDifficulty += SpawningData.DifficultyLevelIncreasePerRound;
            }
        }

        private async void SpawningAsync(CancellationToken cancellationToken)
        {
            while (_currentSpawnCash > SpawningData.EnemiesData.Min(x => x.SpawningPrice) &&
                   !cancellationToken.IsCancellationRequested)
            {
                var availablePoolOfEnemiesData = SpawningData.EnemiesData
                    .Where(x => x.SpawningDifficultyLevel <= _currentSpawnMaxDifficulty &&
                                x.SpawningPrice <= _currentSpawnCash);
                var groupSize = Random.Range(SpawningData.MinGroupSize, SpawningData.MaxGroupSize);
                var enemiesGroup = CreateEnemiesGroup(groupSize, availablePoolOfEnemiesData);

                foreach (var enemy in enemiesGroup)
                {
                    if (_currentSpawnCash - enemy.SpawningPrice <= 0)
                        continue;

                    _enemyFactory.SpawnEnemy(enemy, _positionGetter.GetPosition(), Quaternion.identity);
                    _currentSpawnCash -= enemy.SpawningPrice;
                }

                await UniTask.WaitForSeconds(SpawningData.SpawningIterationDelayInSeconds,
                    cancellationToken: cancellationToken).SuppressCancellationThrow();
            }
        }

        private EnemyDataSO[] CreateEnemiesGroup(int groupSize, IEnumerable<EnemyDataSO> availablePoolOfEnemies)
        {
            var result = new EnemyDataSO[groupSize];
            for (int i = 0; i < groupSize; i++)
            {
                var poolOfEnemies = availablePoolOfEnemies.ToList();
                result[i] = poolOfEnemies[Random.Range(0, poolOfEnemies.Count)];
            }

            result = result.OrderBy(x => x.SpawningPrice).ToArray();
            return result;
        }
    }
}