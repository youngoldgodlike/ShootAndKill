using UnityEngine;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.MotionVariety
{
    public class NavMeshAgentMotion : Motion
    {
        private float _speed;
        protected override void Awake() {
            base.Awake();
            _animator.applyRootMotion = false;
            _speed = _agent.speed;
        }

        protected override void Update() {
            base.Update();
            if (_agent.remainingDistance < _agent.stoppingDistance)
                _agent.velocity = Vector3.zero;
        }
    }
}