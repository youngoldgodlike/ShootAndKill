using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.HealthVariety;
using UnityEngine;
using UnityEngine.Events;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.DamageableVariety
{
    [RequireComponent(typeof(Health))]
    public abstract class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] protected Health _health;
        [SerializeField] protected Animator _animator;
        private Collider _collider;
        private static readonly int Hit = Animator.StringToHash("Hit");

        public UnityEvent onImpact;
        
        private void Awake() {
            _collider = GetComponent<Collider>();
            _health.onPreDie.AddListener(() => _collider.isTrigger = true);
            GetComponent<Enemy>().onSpawn.AddListener(() => _collider.isTrigger = false);
        }

        public void DealDamage(IDamageSource source) {
            _animator.SetTrigger(Hit);
            onImpact.Invoke();
            DamageTaking(source);
        }

        protected abstract void DamageTaking(IDamageSource source);

        public void Initialize(){}
    }
}