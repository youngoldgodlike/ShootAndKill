using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.HealthVariety;
using NaughtyAttributes;
using UnityEngine;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety
{
    [RequireComponent(typeof(Health))]
    public class LevelBoss : Enemy
    {
        [SerializeField, ReadOnly] private Health _hp;

        private void Awake() {
            _hp = GetComponent<Health>();

            _hp.onDieProcessEnd.AddListener(() => { Debug.Log("GAME WIN"); });
        }
    }
}