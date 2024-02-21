using System;
using _Shoot_Kill.Architecture.Scripts.Utilities;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace _Shoot_Kill.Architecture.Scripts.EnemySpawn
{
    public class WavesController : MonoBehaviour
    {
        [SerializeField] private WavesModel _wavesData;
        public CountdownTimer currentWaveTimer { get; private set;}

        public UnityEvent<int> onStartWave;
        public UnityEvent onWavesEnds;
        public UnityEvent onWaveStarts;

        private int waveDuration => _wavesData.currentWave.waveDuration;
        public int currentWave => _wavesData.currentWaveId;

        private void Awake() {
            currentWaveTimer = new CountdownTimer(0);
        }

        private void Start() {
            Invoke(nameof(WavesStart), 1f);
        }

        private void WavesStart() {
            currentWaveTimer.Reset(waveDuration);
            currentWaveTimer.Start();
            currentWaveTimer.OnTimerStop += StartNextWave;
            onWaveStarts.Invoke();
            StartWave(0);
        }
        
        private void Update() {
            currentWaveTimer?.Tick(Time.deltaTime);
        }

        private void StartNextWave() {
            _wavesData.currentWaveId++;
            if (_wavesData.currentWaveId != 5) {
                onStartWave.Invoke(currentWave);
                currentWaveTimer.Reset(waveDuration);
                currentWaveTimer.Start();
            }
            else {
                Debug.Log("WAVES ENDED");
                onWavesEnds.Invoke();
            }
        }
    
        private void StartWave(int waveId) {
            onStartWave.Invoke(waveId);
        }
    }
}
