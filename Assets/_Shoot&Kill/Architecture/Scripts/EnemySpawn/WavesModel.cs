using _Shoot_Kill.Architecture.GameData.Scripts;
using NaughtyAttributes;
using UnityEngine;

namespace _Shoot_Kill.Architecture.Scripts.EnemySpawn
{
    public class WavesModel : MonoBehaviour
    {
        [SerializeField, Expandable] private LevelData _levelData;

        public WaveData currentWave => _levelData.waves[currentWaveId];
        public WaveData[] waves => _levelData.waves;
        [ReadOnly] public int currentWaveId;
    }
}