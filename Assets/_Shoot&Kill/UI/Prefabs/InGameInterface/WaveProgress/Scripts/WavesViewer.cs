using System.Collections.Generic;
using System.Linq;
using _Shoot_Kill.Architecture.Scripts.EnemySpawn;
using _Shoot_Kill.Architecture.Scripts.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Shoot_Kill.UI.Prefabs.InGameInterface.WaveProgress.Scripts
{
    public class WavesViewer : MonoBehaviour
    {
        [SerializeField] private WavesModel _wavesData;
        [SerializeField] private WavesController _wavesController;
        [SerializeField] private RectTransform _progressLabel;
        [SerializeField] private WaveViewPoint _wavePointPrefab;
        [SerializeField] private WaveProgressIdentifier _progressIdentifier;

        private readonly Dictionary<int, WaveViewPoint> _wavesPoints = new();
        private CountdownTimer _wavesTimer;
        private float _progressLength, _secLenght, _wavesDuration;

        private int currentWave => _wavesData.currentWaveId;
        private float midPosX => _progressLabel.rect.width / 2;
        public float wavesProgress => _wavesTimer.Progress;

        public float positionAtProgress {
            get {
                var length = _progressLabel.rect.width * wavesProgress;
                return -(length - midPosX);
            }
        }

        private void Awake() {
            _wavesController.onWaveStarts.AddListener(() => {
                _wavesTimer.Start();
            });
        }

        private async void Start() {
            _progressLength = _progressLabel.rect.width;
            await CreateProgressDisplay();
            _wavesController.currentWaveTimer.OnTimerStart += () => _wavesPoints[currentWave].Activate();
        }

        private void Update() {
            _wavesTimer.Tick(Time.deltaTime);
        }

        private async UniTask CreateProgressDisplay() {
            CalculateOriginalLenght();

            var lastPositionX = 0f;
            bool firstWave = true;

            for (var index = 0; index < _wavesData.waves.Length; index++) {
                var prevWave = index == 0 ? _wavesData.waves[index] : _wavesData.waves[index - 1];
                var point = Instantiate(_wavePointPrefab, _progressLabel.transform);
                var pointRect = point.GetComponent<RectTransform>();

                _wavesPoints[index] = point.GetComponent<WaveViewPoint>();
                _wavesPoints[index].Initialize(index + 1);

                if (firstWave) {
                    firstWave = false;
                    var initialPos = new Vector3(-_progressLength / 2, 0f, 0f);
                    pointRect.localPosition = initialPos;

                    var progressIdentifier = Instantiate(_progressIdentifier, _progressLabel.transform);
                    Debug.Log(initialPos);
                    progressIdentifier.GetComponent<RectTransform>().localPosition = initialPos;
                    progressIdentifier.GetComponent<WaveProgressIdentifier>().Initialize(this);
                    
                    continue;
                }

                var posX = lastPositionX + _secLenght * prevWave.waveDuration;
                var xMaxMin = posX / _progressLabel.rect.width;
                lastPositionX = posX;

                pointRect.anchorMax = new Vector2(xMaxMin, pointRect.anchorMax.y);
                pointRect.anchorMin = new Vector2(xMaxMin, pointRect.anchorMin.y);
                
                await UniTask.Yield();
            }
        }

        private void CalculateOriginalLenght() {
            _wavesDuration = _wavesData.waves.Sum(wave => wave.waveDuration);

            _wavesDuration -= _wavesData.waves[^1].waveDuration;
            _secLenght = _progressLength / _wavesDuration;
            _wavesTimer = new CountdownTimer(_wavesDuration);
        }
    }
}
