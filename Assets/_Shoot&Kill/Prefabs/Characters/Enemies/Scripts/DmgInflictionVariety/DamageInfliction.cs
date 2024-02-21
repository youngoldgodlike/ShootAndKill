using System.Collections;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.DmgInflicitonVariety
{
    public abstract class DamageInfliction : MonoBehaviour, IDamageSource
    {
        [Header("Base Dependencies")]
        [SerializeField] protected Animator _animator;
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField,ReadOnly] protected Transform _target;
        
        [Header("Base Parameters")]
        [SerializeField, ReadOnly] protected float _damage;
        [SerializeField, ReadOnly] protected float _attackDistance;
        [SerializeField] protected LayerMask _obstructions;
        private Coroutine _lookAtPlayer;
        
        protected static readonly int Attack = Animator.StringToHash("Attack");
        public GameObject damageDealer => gameObject;

        protected Vector3 position {
            get => transform.position;
            set => transform.position = value;
        }
        protected Quaternion rotation => transform.rotation;

        public float damage => _damage;
        public float attackDistance => _attackDistance;

        /// <summary>
        /// Вызов реализации атаки.
        /// Этот метод должен вызываться сугубо из анимации.
        /// </summary>
        public abstract void Assault();

        private void Update() {
            if (_agent.hasPath) {
                if (_agent.remainingDistance <= 0) return;
                var isAttack = _agent.remainingDistance < _attackDistance && TargetInSight();
                _animator.SetBool(Attack, isAttack);
            }
        }

        private bool TargetInSight() {
            var dir = _target.position - position;
            var ray = new Ray(position, dir);
            return !Physics.Raycast(ray, _agent.remainingDistance, _obstructions);
        }

        public virtual void Initialize(float dmg, float attackDist,Transform target) {
            _damage = dmg;
            _attackDistance = attackDist;
            _target = target;
        }
    }
}