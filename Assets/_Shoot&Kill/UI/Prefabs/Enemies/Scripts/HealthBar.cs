using System;
using _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.HealthVariety;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Health _hp;
        [SerializeField] protected GameObject _background;
        [SerializeField] protected Image _hpCurrent, _hpBelated;
        
        protected virtual void Awake() {
            _background.SetActive(false);
            _hpCurrent.fillAmount = 1f;
            _hpBelated.fillAmount = 1f;

            if (_hp.IsUnityNull()) {
                try {
                    _hp = GetComponent<Health>();
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    throw;
                }
            }
            _hp.onHealthChange.AddListener(ChangeView);
        }
        
        private void ChangeView() {
            _background.SetActive(_hp.health < _hp.maxHealth && _hp.health != 0);
            _hpCurrent.fillAmount = _hp.health / _hp.maxHealth;
        }
        
        protected virtual void Update() {
            var hpChangeDiff = _hpBelated.fillAmount - _hpCurrent.fillAmount;
            if (hpChangeDiff > 0) {
                var changeSpeed = Mathf.Clamp(hpChangeDiff, 0.5f, 1f);
                _hpBelated.fillAmount -= changeSpeed * Time.deltaTime;
            }
        }
    }
}