using System;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety;

namespace _Shoot_Kill.Architecture.Scripts.EnemySpawn
{
    public class EnemiesPool : PersistencePool<Enemy>
    {
        public EnemiesPool(Func<Enemy> createAction, int capacity) : 
            base(GetAction, ReturnAction, createAction, capacity) { }

        private static void GetAction(Enemy enemy) {
            //Debug.Log($"{enemy.name} get from pool");
            enemy.onSpawn.Invoke();
            enemy.gameObject.SetActive(true);
        }

        private static void ReturnAction(Enemy enemy) {
            //Debug.Log($"{enemy.name} returned to pool");
            enemy.gameObject.SetActive(false);
        }
        
    }
}