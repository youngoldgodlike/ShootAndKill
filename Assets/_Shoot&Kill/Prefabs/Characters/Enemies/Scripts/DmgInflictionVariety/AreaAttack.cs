using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.DmgInflicitonVariety;
using UnityEngine;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.DmgInflictionVariety
{
    public class AreaAttack : DamageInfliction
    {
        [Header("AreaAttack Params")]
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private ParticleSystem _attackParticle;
        public float radius => _radius;

        public override void Assault() {
            var playerCollider = Physics.OverlapSphere(position, _radius, _playerMask);

            var emission = _attackParticle.emission;
            var shape = _attackParticle.shape;
            var main = _attackParticle.main;
            shape.radius = _radius;
            main.startSize = _radius;
            emission.enabled = true;
            
            
            _attackParticle.Play();

            if(playerCollider.Length == 0) return;
            
            var player = playerCollider[0].GetComponent<IDamageable>();
            player.DealDamage(this);
        }
    }
}
