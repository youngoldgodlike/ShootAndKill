using NaughtyAttributes;
using UnityEngine;

namespace _Shoot_Kill.UI.Prefabs.InGameInterface.WaveProgress.Scripts
{
    public class WaveProgressIdentifier : MonoBehaviour
    {
        [SerializeField, ReadOnly] private WavesViewer _wavesViewer;
        [SerializeField] private RectTransform _transform;

        public void Initialize(WavesViewer viewer) {
            _wavesViewer = viewer;
        }

        private void Update() {
            _transform.localPosition = new Vector3(_wavesViewer.positionAtProgress, _transform.localPosition.y, 0f);
        }
    }
}