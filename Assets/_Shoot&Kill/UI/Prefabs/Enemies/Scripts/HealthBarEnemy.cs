using NaughtyAttributes;
using UnityEngine;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts
{
    public class HealthBarEnemy : HealthBar
    {
        [SerializeField, ReadOnly] private Transform _camera;

        protected override void Awake() {
            base.Awake();
            _camera = Camera.main.transform;
        }

        protected override void Update() {
            base.Update();
            _background.transform.rotation = _camera.rotation;
        }
    }
}