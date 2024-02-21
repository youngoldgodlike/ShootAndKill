using UnityEngine;
using UnityEngine.Events;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.EnemiesVariety
{
    public abstract class Enemy : MonoBehaviour
    {
        public UnityEvent onSpawn = new();
    }
}