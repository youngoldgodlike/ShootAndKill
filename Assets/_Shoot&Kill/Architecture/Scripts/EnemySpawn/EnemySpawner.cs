using System;
using System.Collections.Generic;
using System.Threading;
using _Shoot_Kill.Architecture.GameData.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Shoot_Kill.Architecture.Scripts.EnemySpawn
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        [SerializeField] protected WavesModel _wavesData;
        [SerializeField] protected WavesController _wavesController;
        [SerializeField] protected Transform _player;
        [SerializeField] private Transform _enemiesParent;
        [SerializeField, MinMaxSlider(20f,100f)] protected Vector2 _spawnRange;

        protected readonly EnemyBuilder builder = new();
        protected readonly Dictionary<EnemyData, EnemiesPool> enemiesPools = new();
        protected readonly Dictionary<EnemiesPool, EnemySettings> enemiesSettings = new();
        protected readonly Dictionary<EnemyData, Transform> enemiesParents = new();
        protected readonly Dictionary<int, CancellationTokenSource> waveCts = new();

        protected int currentWave => _wavesData.currentWaveId;
        protected float statsMultiplier => _wavesData.waves[currentWave].statsMultiplier;
        public Vector2 rangeSpawn => _spawnRange;

        private void Awake() {
            _wavesController.onStartWave.AddListener(SpawnWave);
        }

        private async void SpawnWave(int waveId) {
            Debug.Log($"Wave: {waveId}");
            waveCts[waveId] = new CancellationTokenSource();
            await CreatePools(waveId);
            StartSpawn();
        }

        private void StartSpawn() {
            foreach (var pool in enemiesPools.Values) {
                SpawnProcess(pool);
            }
        }

        protected abstract UniTask CreatePools(int waveId);
        protected abstract UniTask SpawnProcess(EnemiesPool pool);
        protected abstract Enemy CreateEnemy(EnemyData enemy);

        protected void CreatePool(EnemySettings enemy) {
            var parent = Instantiate(new GameObject($"Wave{currentWave}: " + enemy.enemyData.fullname), _enemiesParent);
            enemiesParents[enemy.enemyData] = parent.transform;
            var newPool = new EnemiesPool(() => CreateEnemy(enemy.enemyData), enemy.requiredQuantity);

            enemiesPools[enemy.enemyData] = newPool;
            enemiesSettings[newPool] = enemy;
        }
        
        protected virtual Vector3 GetSpawnPos() {
            var startPos = _player.position;
            var randomAngle = Random.Range(0, 359);
            var randomRange = Random.Range(_spawnRange.x, _spawnRange.y);

            var xPos = startPos.x + randomRange * MathF.Cos(randomAngle);
            var zPos = startPos.z + randomRange * MathF.Sin(randomAngle);
            var spawnPos = new Vector3(xPos, startPos.y, zPos);
            
            return spawnPos;
        }
    }
}