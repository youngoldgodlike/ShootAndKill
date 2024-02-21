using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NaughtyAttributes;
using UnityEngine;

namespace _Shoot_Kill.Architecture.GameData.Scripts
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]
    public class LevelData : ScriptableObject
    {
        public WaveData[] waves = new WaveData[5];
    }

    [Serializable]
    public struct WaveData
    {
        [SerializeField] private List<EnemySettings> _regularEnemies;
        [SerializeField] private List<EnemySettings> _onetimeEnemies;
        [SerializeField, Min(0f)] private int _waveDuration;
        [SerializeField, Min(0f)] private float _statsMultiplier;
        [SerializeField] private WaveType _waveType;
        
        
        public ReadOnlyCollection<EnemySettings> regularEnemies => _regularEnemies.AsReadOnly();
        public ReadOnlyCollection<EnemySettings> onetimeEnemies => _onetimeEnemies.AsReadOnly();
        public float statsMultiplier => _statsMultiplier;
        public int waveDuration => _waveDuration;
        private WaveType waveType => _waveType;
    }
    
    [Serializable]
    public struct EnemySettings
    {
        [SerializeField, Expandable] private EnemyData _enemyData;
        [SerializeField, Min(0f)] private int _requiredQuantity;
        [SerializeField, Min(0f)] private float _spawnCooldown;

        public float spawnCooldown => _spawnCooldown;
        public EnemyData enemyData => _enemyData;
        public int requiredQuantity => _requiredQuantity;
    }

    public enum WaveType
    {
        Wave,Boss,BossWave
    }
}
