using Assets.Prefabs.Characters.MainHero.Scripts;
using Assets.Prefabs.Characters.MainHero.Scripts.XPSystem;
using UnityEngine;
using Random = System.Random;

namespace Assets.Prefabs.ExpStar.Scripts
{
    public class ExpStar : MonoBehaviour
    {
        [SerializeField] private float _expAmount = 10f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _maxSpeed = 100f;
        
        private float _rotationSpeed;
        private float _defaultSpeed;
        private ExpSystem _expSystem;
        private float _step = 0.01f;
        private Transform _target;

        private bool _isTouch;
        
        private void Awake()
        {
            Random random = new Random();
            _rotationSpeed = random.Next(50, 100);
            
            _defaultSpeed = _moveSpeed; 
            _target = HeroMovement.Instance.transform;
            _expSystem = ExpSystem.Instance;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
            
            if (!_isTouch) return;

            _moveSpeed = Mathf.Lerp(_moveSpeed,_maxSpeed, _step);
            
            transform.position = Vector3.MoveTowards(
                transform.position, _target.position,
                _moveSpeed * Time.deltaTime);
        }


        private void OnCollisionEnter(Collision collision)
        {
            _expSystem.AddExp(_expAmount);
            DisposeObject();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _isTouch = true;
        }

        private void DisposeObject()
        {
            gameObject.SetActive(false);
            _moveSpeed = _defaultSpeed;
            _isTouch = false;
        }
    }
}
