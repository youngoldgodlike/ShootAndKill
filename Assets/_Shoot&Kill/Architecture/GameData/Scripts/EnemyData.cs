using NaughtyAttributes;
using UnityEngine;

namespace _Shoot_Kill.Architecture.GameData.Scripts
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField, ShowAssetPreview, Required] private GameObject _prefab;
        [SerializeField] private string _fullname = "Nameless enemy";
        [SerializeField, Min(0f)] private float _health, _damage, _attackDistance;
        [SerializeField] private float _stopDistance = 1f, _motionSpeed = 1f;

        public GameObject enemyPrefab => _prefab;
        public string fullname => _fullname;
        public float health => _health;
        public float damage => _damage;
        public float attackDistance => _attackDistance;
        public float stopDistance => _stopDistance;
        public float motionSpeed => _motionSpeed;

        private void OnValidate() {
            _attackDistance = Mathf.Clamp(_attackDistance, _stopDistance + 0.5f, Mathf.Infinity);
        }
    }
}