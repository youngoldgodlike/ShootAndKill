using NaughtyAttributes;
using UnityEngine;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.Attachments
{
    public class MeshChanger : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _hitMat;

        public void SetHitMat(float duration) {
            SetHitMat();
            Invoke(nameof(SetDefaultMat), duration);
        }

        [Button("Hit Material")]
        private void SetHitMat() => _meshRenderer.material = _hitMat;

        [Button("Default Material")]
        public void SetDefaultMat() {
            _meshRenderer.material = _defaultMaterial;
        }
    }
}