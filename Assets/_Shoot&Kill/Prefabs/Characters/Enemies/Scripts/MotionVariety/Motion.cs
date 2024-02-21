using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.HealthVariety;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.MotionVariety
{
    public abstract class Motion : MonoBehaviour
    {
        [Header("Base Dependencies")] 
        [SerializeField] protected Health _hp;
        [SerializeField] protected Enemy _enemy;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected NavMeshAgent _agent;

        [Header("Base Parameters")] 
        [SerializeField, ReadOnly] protected float _stopDistance;
        [SerializeField, ReadOnly] protected float _motionSpeed;
        [SerializeField, ReadOnly] protected Transform _target;
        [SerializeField] protected MotionVariety _motion;
        protected MotionBehavior _motionBehavior;

        [Header("Agent")]
        [SerializeField] protected bool _updateRotation = true;
        [SerializeField] protected bool _updatePosition;

        protected virtual void Awake() {
            if (!_hp) _hp = GetComponent<Health>();
            if (!_enemy) _enemy = GetComponent<Enemy>();
            
            _hp.onPreDie.AddListener(DisableMotion);
            _enemy.onSpawn.AddListener(EnableMotion);
        }

        protected virtual void Update() {
            _motionBehavior.CalculatePath();
        }

        protected float velocity {
            get {
                if (!_agent.IsUnityNull()) {
                    return _agent.velocity.magnitude / _agent.speed;
                }

                return 0f;
            }
        }

        public virtual void Initialize(float stopDist, Transform target, float motionSpeed = 1f) {
            _stopDistance = stopDist;
            _target = target;
            _motionSpeed = motionSpeed;
            
            _animator.speed = _motionSpeed;
            SetAgentSettings();
            SetMotionBehavior();
        }

        protected void DisableMotion() {
            _agent.updatePosition = false;
            _agent.updateRotation = false;
        }

        protected void EnableMotion() {
            _agent.updatePosition = _updatePosition;
            _agent.updateRotation = _updateRotation;

        }

        private void SetAgentSettings() {
            _agent.stoppingDistance = _stopDistance;
            _agent.updatePosition = _updatePosition;
            _agent.updateRotation = _updateRotation;
            
            _agent.speed *= _motionSpeed;
            _agent.angularSpeed *= _motionSpeed;
            _agent.acceleration *= _motionSpeed;
        }

        private void SetMotionBehavior() {
            if (_motion == MotionVariety.Approach) ;
                _motionBehavior = new Approach(_agent, _target);
        }
        
        public enum MotionVariety
        {
            None, Approach, KeepDistance
        }
    }
}