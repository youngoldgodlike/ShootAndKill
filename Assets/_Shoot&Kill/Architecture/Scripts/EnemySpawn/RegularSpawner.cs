using _Shoot_Kill.Architecture.GameData.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.HealthVariety;
using Cysharp.Threading.Tasks;


namespace _Shoot_Kill.Architecture.Scripts.EnemySpawn
{
    public class RegularSpawner : EnemySpawner
    {
        protected override async UniTask CreatePools(int waveId) {
            foreach (var enemy in _wavesData.waves[waveId].regularEnemies) {
                CreatePool(enemy);
                await UniTask.Yield();
            }
        }

        protected override async UniTask SpawnProcess(EnemiesPool pool) {
            var ct = waveCts[currentWave].Token;
            
            while (true) {
                Enemy obj = null;
                await UniTask.WaitForSeconds(enemiesSettings[pool].spawnCooldown, cancellationToken: ct);
                await UniTask.WaitUntil(() => pool.TryGetItem(out obj));

                obj.transform.position = GetSpawnPos();
            }
        }

        protected override Enemy CreateEnemy(EnemyData data) {
            var enemy = builder
                .Create(data)
                .WithTarget(_player)
                .WithStatsMultiplier(statsMultiplier)
                .WithParent(enemiesParents[data])
                .Build();
        
            enemy.GetComponent<Health>().onDieProcessEnd.AddListener(() => {
                enemiesPools[data].ReturnInPool(enemy);
            });
            
            return enemy;
        }
    }
}
