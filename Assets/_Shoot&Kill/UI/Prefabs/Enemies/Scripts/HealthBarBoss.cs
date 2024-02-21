using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety;
using UnityEngine;

namespace _Shoot_Kill.UI.Prefabs.Enemies.Scripts
{
    public class HealthBarBoss : HealthBar
    {
        [SerializeField] private Enemy _enemy;

        private void Start() {
            _enemy.onSpawn.AddListener(()=>_background.SetActive(true));
        }
    }
}
