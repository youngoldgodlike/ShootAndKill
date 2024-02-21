using System;
using Assets.Architecture.Scripts.Utils;
using Assets.UI.Architecture.Scripts;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;

using UnityEngine;

namespace Assets.Prefabs.Guns.Scripts
{
    [RequireComponent(typeof(PlaySoundsComponent))]
    public class ProjectileAttack : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _storeProgressBar;
        [SerializeField] private ProjectilePool _pool;
        [SerializeField] private Transform _weaponMuzzle;
        [SerializeField] private ParticleSystem _muzzleVfx;
        
        [Header("Properties")]
        [SerializeField, Min(0f)] private float _force = 10f;
        [SerializeField, Min(0f)] private int _storeCount = 30;
        [SerializeField] private float _reloadingTime;
        [SerializeField] private ForceMode _forceMode = ForceMode.Impulse;
        [Tooltip("Reload time in seconds")]
        [SerializeField] private Cooldown _rate;

        private PlaySoundsComponent _sounds;
        private Animator _animator;
        private int _defaultStore;
        private bool _isReload;

        private static readonly int IsAttack = Animator.StringToHash("isAttack");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _defaultStore = _storeCount;
            _sounds = GetComponent<PlaySoundsComponent>();
        }
        
        [Button("PerformAttack")]
        public void PerformAttack()
        {
            if (!_rate.IsReady) return;
            if (_isReload) return;

            if (_storeCount > 0 )
            {
                Attack();
                _rate.Reset();

                if (_storeCount <= 0)
                    Reload().Forget();
            }
            else
                Reload().Forget();
        }
        
        private void Attack()
        {
            _animator.SetTrigger(IsAttack);
            
            _muzzleVfx.Play();
            _sounds.Play();
            
            var projectile = _pool.GetObject(_weaponMuzzle);
            projectile.rigidBody.AddForce(_weaponMuzzle.forward * _force, _forceMode);
            projectile.transform.rotation = transform.rotation;

            _storeCount--;
            _storeProgressBar.SetProgress(_storeCount, _defaultStore);
        }

        private async UniTaskVoid Reload()
        {
            _isReload = true;
            _storeProgressBar.StartCountdown(_reloadingTime).Forget();
            
            await UniTask.Delay(TimeSpan.FromSeconds(_reloadingTime));
            _storeCount = _defaultStore;
            _isReload = false;
        }
        

        public void SetRate(float percent) => _rate.Delay -= _rate.Delay / 100 * percent;
        public void SetTimeReload(float percent) => _reloadingTime -= _reloadingTime / 100 * percent;
        public void SetStoreCount(int count) => _defaultStore += count;
        
    }
}
