using _Shoot_Kill.Architecture.Scripts.EnemySpawn;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.DmgInflicitonVariety;
using UnityEngine;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.DmgInflictionVariety
{
    public class ProjectileShoot : DamageInfliction
    {
        [Header("Projectile Settings")]
        [SerializeField, InspectorName("Projectile")] private EnemyProjectile _prefab;
        [SerializeField, InspectorName("Projectile speed")] private float _speed;
        [SerializeField, InspectorName("Projectile Spawn Pos")] private Transform _spawnPos;
        [SerializeField] private int _poolCapacity;
        [SerializeField] private Transform _storage;
        private EnemyProjectilesPool _pool;

        private void Awake() {
            CreatePool();
        }

        private void CreatePool() {
            var storage = new GameObject($"{_prefab.name} storage");
            _storage = storage.transform;
            _pool = new EnemyProjectilesPool(Create, _poolCapacity);
        }

        private EnemyProjectile Create() {
            var projectile = Instantiate(_prefab, _storage);
            projectile.onHit.AddListener(() => _pool.ReturnInPool(projectile));
            
            return projectile;
        }

        public override void Assault() {
            var projectile = _pool.GetItem();
            projectile.Initialize(_target.position, _speed, _spawnPos.position,_damage);
        }
    }
}