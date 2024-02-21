using System;
using _Shoot_Kill.Architecture.GameData.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.DmgInflicitonVariety;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.HealthVariety;
using UnityEngine;
using Motion = _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.MotionVariety.Motion;
using Object = UnityEngine.Object;

namespace _Shoot_Kill.Architecture.Scripts.EnemySpawn
{
    public class EnemyBuilder
    {
        private EnemyData _data;
        private Transform _target, _parent;
        private float _statsMultiplier = 1f;

        #region Stats

        private float health => _data.health * _statsMultiplier;
        private float damage => _data.damage * _statsMultiplier;
        private float attackDist => _data.attackDistance;
        private float stopDistance => _data.stopDistance;
        private float motionSpeed => _data.motionSpeed;
        
        #endregion
        
        public EnemyBuilder Create(EnemyData data) {
            _data = data;
            return this;
        }

        public EnemyBuilder WithTarget(Transform target) {
            _target = target;
            return this;
        }

        public EnemyBuilder WithStatsMultiplier(float multiplier) {
            if (multiplier < 0) 
                throw new ArgumentOutOfRangeException(nameof(multiplier));
            
            _statsMultiplier = multiplier;
            return this;
        }

        public EnemyBuilder WithParent(Transform parent) {
            _parent = parent;
            return this;
        }

        public Enemy Build() {
            var enemy = Object.Instantiate(_data.enemyPrefab,_parent);
            enemy.name = _data.fullname;

            enemy.GetComponentInChildren<Health>().Initialize(health);
            enemy.GetComponentInChildren<DamageInfliction>().Initialize(damage, attackDist, _target);
            enemy.GetComponentInChildren<Motion>().Initialize(stopDistance, _target, motionSpeed);

            Clear();
            return enemy.GetComponentInChildren<Enemy>();
        }

        private void Clear() {
            _data = null;
            _target = null;
            _parent = null;
            _statsMultiplier = 1f;
        }
    }
}