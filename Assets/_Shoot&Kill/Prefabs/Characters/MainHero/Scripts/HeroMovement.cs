using _Shoot_Kill.Architecture.Scripts;
using Assets.Architecture.Scripts.Utils;
using Assets.UI.Architecture.Scripts;
using UnityEngine;

namespace Assets.Prefabs.Characters.MainHero.Scripts
{
    public class HeroMovement : Singleton<HeroMovement>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerRayCast;
        [SerializeField] private ProgressBarWidget _dashProgressBar;
        
        [Header("Properties")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _dashForce = 10f;
        [SerializeField] private Cooldown _dashDelay;

        private float _defaultSpeed;
        private Vector3 _direction;
        private Rigidbody _rigidBody;
        private Animator _animator;
        private readonly float _dividerCurvature = 1.25f;
       
        private static readonly int IsForward = Animator.StringToHash("isRun");
        private static readonly int IsRight = Animator.StringToHash("isRight");

        private bool isMove => _direction != Vector3.zero;
        
        protected override void Awake()
        {
            base.Awake();
            
            _rigidBody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _defaultSpeed = _speed;
        }
        
        private void FixedUpdate()
        {
            CalculateVelocity();
            CalculateRotateDirection();
        }

        public void SetDirection(Vector3 direction) => _direction = direction;

        public void Dash()
        {
            if (!_dashDelay.IsReady) return;    
        
            _dashProgressBar.StartCountdown(_dashDelay.Delay).Forget();
            _rigidBody.AddForce(_direction * _dashForce, ForceMode.Impulse);
            _dashDelay.Reset();
        }

        public void UpDashDelay(float percent) => _dashDelay.Delay -= (_dashDelay.Delay / 100) * percent;
        public void UpSpeed(float percent) => _speed += _defaultSpeed / 100 * percent;

        private void CalculateVelocity()
        {
            _rigidBody.MovePosition(transform.position + _direction.normalized * Time.deltaTime * _speed);

            var directionForward = Vector3.Dot(_direction, transform.forward);
            var directionRight = Vector3.Dot(_direction, transform.right);
            
            _animator.SetFloat(IsForward, directionForward);
            _animator.SetFloat(IsRight, directionRight);
        }

        private void CalculateRotateDirection()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 50, _layerRayCast))
            {
                var lookDirection = hitInfo.point - transform.position;
                lookDirection.y = transform.position.y;

                var rotation = Quaternion.LookRotation(lookDirection);
                _rigidBody.MoveRotation(rotation);
            }
        }
        
        
        
    }
}
