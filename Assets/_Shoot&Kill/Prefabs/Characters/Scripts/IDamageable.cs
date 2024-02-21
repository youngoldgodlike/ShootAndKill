using UnityEngine;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts
{
    public interface IDamageable
    {
        public void DealDamage(IDamageSource source);
    }

    public interface IDamageSource
    {
        public float damage { get; }
        public GameObject damageDealer { get; }
    }
}