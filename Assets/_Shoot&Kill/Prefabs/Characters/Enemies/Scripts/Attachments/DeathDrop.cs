using System;
using System.Collections.Generic;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.HealthVariety;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.Attachments
{
    public class DeathDrop : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Transform _dropPlace;
        [SerializeField] private List<DropList> _dropItems; 
        
        private void Awake() {
            _health.onDieProcessEnd.AddListener(() => {
                foreach (var item in _dropItems) {
                    var toDrop = Random.Range(0, 100) <= item.dropChance;
                    if(toDrop) Drop(item.item); 
                }
            });
        }
        
        private void Drop(GameObject obj) {
            Instantiate(obj, _dropPlace.position, Quaternion.identity);
        }
    }

    [Serializable]
    public struct DropList
    {
        [SerializeField] private GameObject _item;
        [SerializeField, Range(0, 100)] private int _dropChance;

        public GameObject item => _item;
        public int dropChance => _dropChance;
    }
}
