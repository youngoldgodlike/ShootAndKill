using _Shoot_Kill.Architecture.GameData.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety;
using Cysharp.Threading.Tasks;

namespace _Shoot_Kill.Architecture.Scripts.EnemySpawn
{
    public class OneTimeSpawner : EnemySpawner
    {
        protected override async UniTask CreatePools(int waveId) {
            foreach (var enemy in _wavesData.waves[waveId].onetimeEnemies) {
                CreatePool(enemy);
                await UniTask.Yield();
            }
        }

        protected override async UniTask SpawnProcess(EnemiesPool pool) {
            await UniTask.WaitForSeconds(enemiesSettings[pool].spawnCooldown);
            
            while (pool.TryGetItem(out var obj)) {
                obj.transform.position = GetSpawnPos();
                
                await UniTask.WaitForSeconds(enemiesSettings[pool].spawnCooldown);
            }
        }

        protected override Enemy CreateEnemy(EnemyData data) {
            var enemy = builder
                .Create(data)
                .WithTarget(_player)
                .WithStatsMultiplier(statsMultiplier)
                .WithParent(enemiesParents[data])
                .Build();
            return enemy;
        }
    }
}