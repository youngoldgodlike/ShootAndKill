using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Shoot_Kill.UI.Prefabs.InGameInterface.WaveProgress.Scripts
{
    public class WaveViewPoint : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI _waveId;
        [SerializeField] private Image _point;

        public void Initialize(int waveId) {
            _waveId.text = $"{waveId}";

        }
        
        public void Activate() {
            _point.color = Color.red;
        }
    }
}