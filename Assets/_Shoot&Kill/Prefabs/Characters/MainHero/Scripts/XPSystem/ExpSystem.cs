using System;
using System.ComponentModel;
using _Shoot_Kill.Architecture.Scripts;
using Assets.UI.Architecture.Scripts;
using NaughtyAttributes;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Assets.Prefabs.Characters.MainHero.Scripts.XPSystem
{
    public class ExpSystem : Singleton<ExpSystem>
    {
        [SerializeField] private ProgressBarWithText _bar;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        
        [FormerlySerializedAs("_maxExpBar")] [SerializeField] private float _maxExp = 100f;
        [SerializeField] private int _growthFactor = 10;
        [SerializeField, NaughtyAttributes.ReadOnly] private float _expProgress = 0;

        [SerializeField] public UnityEvent onLevelUp = new();

        private int _currentLevel = 1;
        
        protected override void Awake()
        {
            base.Awake();
            _bar.SetProgressWithText(_expProgress, _maxExp, $"{Convert.ToInt32(_expProgress)}/{Convert.ToInt32(_maxExp)}");
            _currentLevelText.text = $"Level: {_currentLevel}";
        }

        public void AddExp(float exp)
        {
            _expProgress += exp;

            if (_expProgress >= _maxExp)
            {
                _currentLevel++;
                _expProgress = (_maxExp - _expProgress) * -1;
                _maxExp += _maxExp / 100 * _growthFactor;

                _currentLevelText.text = $"Level: {_currentLevel}";
                onLevelUp?.Invoke();
            }

            _bar.SetProgressWithText(_expProgress, _maxExp, $"{Convert.ToInt32(_expProgress)}/{Convert.ToInt32(_maxExp)}");
        }
    }
}
