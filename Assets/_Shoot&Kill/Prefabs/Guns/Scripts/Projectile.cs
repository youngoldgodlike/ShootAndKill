using System;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts;
using UnityEngine;

namespace Assets.Prefabs.Guns.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour, IDamageSource
    {
        [SerializeField] private DamageGunConfig _guns;
        [SerializeField, Min(0)] private float _damage = 10f;
        [field: SerializeField] public Rigidbody rigidBody { get; private set; }

        public float damage => _damage;
        public GameObject damageDealer => gameObject;
        
        private bool _isDispose;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable)) {
                damageable.DealDamage(this);
            }

            DisposeProjectile();
        }

        protected virtual void DisposeProjectile()
        {
            gameObject.SetActive(false);
            rigidBody.velocity = Vector3.zero;
        }

        private void OnEnable()
        {
            SetDamage();
        }
        public void SetDamage() => _damage = _guns.MiniGunDamage;
    }
}
