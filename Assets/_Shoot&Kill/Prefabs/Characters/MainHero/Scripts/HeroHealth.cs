using System;
using _Shoot_Kill.Architecture.Scripts;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts;
using Assets.UI.Architecture.Scripts;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Prefabs.Characters.MainHero.Scripts
{  
    public class HeroHealth : Singleton<HeroHealth>, IDamageable
    {
        [SerializeField] private float _health;
        [SerializeField] private ProgressBarWithText _bar;
        [SerializeField] private Image _hitImage;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _hitColor;

        public  UnityEvent OnDamage;
        public  UnityEvent OnHeal;
        public  UnityEvent OnDie;

        private float _maxHealth;
        private float _rednessTimer = 0.2f;

        public bool isDie => _health <= 0;
        
        protected override void Awake()
        {
            base.Awake();
            _maxHealth = _health;
            SetProgress();
        }
        

        public void DealDamage(IDamageSource source)
        {

            
            _health -= source.damage;
            SetProgress();
            
            OnAttackRedness().Forget();
            OnDamage?.Invoke();
            
            if (!isDie) return;

            _health = 0;
            SetProgress();
            OnDie?.Invoke();
        }

        public void UpHealth(float value)
        {
            _maxHealth += value;
            _health += value;
            SetProgress();
        }

        public void Heal(float value)
        {
            if (_health >= _maxHealth) return;

            _health += value;

            if (_health > _maxHealth)
                _health = _maxHealth;
            
            SetProgress();
        }

        [Button("Hit")]
        private void TestDie()
        {
            _health -= 10;
            SetProgress();
            
            OnAttackRedness().Forget();
            OnDamage?.Invoke();
            
            if (!isDie) return;

            _health = 0;
            SetProgress();
            OnDie?.Invoke();
        }

        private async UniTaskVoid OnAttackRedness()
        {
            float time = 0f;

            while (time < _rednessTimer)
            {
                time += Time.deltaTime;

                _hitImage.color = Color.Lerp(_defaultColor, _hitColor, time / _rednessTimer);

                await UniTask.Yield();
            }

            _hitImage.color = _defaultColor;
        }
        
        private void SetProgress() => _bar.SetProgressWithText(_health , _maxHealth,$"{Convert.ToInt32(_health)} / {Convert.ToInt32(_maxHealth)}");
    }
}
