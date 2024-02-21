using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.MotionVariety
{
    public class AnimationRootMotion : Motion
    {
        [Header("Dependencies")]
        [SerializeField] protected Transform _parent;
        
        private float _yAgentDisplacement;
        private static readonly int Velocity = Animator.StringToHash("Velocity");

        protected override void Awake() {
            base.Awake();
            _animator.applyRootMotion = true;
        }

        protected override void Update() {
            base.Update();
            _animator.SetFloat(Velocity, velocity);
        }

        private void Start() {
            _yAgentDisplacement = _agent.nextPosition.y;
            ResetAfterAnimator().Forget();
        }
        
        private void OnAnimatorMove() {
            Quaternion rootRotation = _animator.rootRotation;
            Vector3 rootPosition = _animator.rootPosition; // позиция анимации, которая должна поставится
            var nextAgentY = _agent.nextPosition.y - _yAgentDisplacement; // позиция агента, которая должна поставится
            var rootPosY = rootPosition.y - _parent.position.y;

            // Debug.Log($"RtPos: {rootPosition.y} RtDiff:{rootPosYDiff}, NxtAgntPos: {nextAgentY}, DIFF: {Ydiff}");

            rootPosition.y = nextAgentY;
            _parent.position = rootPosition;
            
            var newPos = new Vector3(_parent.position.x, 
                rootPosY + nextAgentY,
                _parent.position.z);

            transform.position = newPos;
            transform.rotation = rootRotation;


            _agent.nextPosition = _parent.position;
            
            var agentPos = new Vector3(_agent.nextPosition.x, transform.position.y, _agent.nextPosition.z); 
            transform.position = agentPos;

            _animatorPass = true;
        }

        [Button("Reset Y Pos")]
        public void ResetYPos() {
            ResetAfterAnimator().Forget();
        }

        private bool _animatorPass;
        private async UniTask ResetAfterAnimator() {
            _animatorPass = false;
            await UniTask.WaitUntil(() => _animatorPass = true);
            _animatorPass = false;
            var locPos = transform.localPosition;
            transform.localPosition = new Vector3(locPos.x, 0f, locPos.z);
        }
    }
}