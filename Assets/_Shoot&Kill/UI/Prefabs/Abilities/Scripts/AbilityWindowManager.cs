using System;
using System.Collections.Generic;
using _Shoot_Kill.Architecture.Scripts;
using Assets.Prefabs.Characters.MainHero.Scripts;
using Assets.Prefabs.Characters.MainHero.Scripts.XPSystem;
using UnityEngine;
using Random = System.Random;

namespace Assets.UI.Prefabs.Abilities.Scripts
{
    public class AbilityWindowManager : Singleton<AbilityWindowManager>
    {

        [SerializeField] private Transform _abilityContainer;
        [SerializeField] private Transform _window;
        [SerializeField] private HeroMovement _movement;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private AbilData[] _abilityData; 

        [SerializeField] private List<AbilityTag> _selectedTags = new List<AbilityTag>();

        [SerializeField] private List<AbilityCell> _abilityCells = new List<AbilityCell>();
        
        private AbilityCell _selectedAbility;
        
        
        private int _abilityCount = 3;

        protected override void Awake()
        {
            base.Awake();
            ExpSystem.Instance.onLevelUp.AddListener(ShowAbility);
        }
        
        private void ShowAbility()
        {
            GameSession.instance.UiIsActive = true;
            _window.gameObject.SetActive(true);
            Time.timeScale = 0f;
            
            CalculateLot();
        }

        public void ChoiceAbility(Action action, AbilityCell selected)
        {
            _selectedAbility = selected;
            
            foreach (var ability in _abilityCells)
                ability.Hide();

            action?.Invoke();
        }

        private void CalculateLot()
        {
            Random random = new Random();
            
            
            for (int i = 0; i < _abilityCount; i++)
            {
                var data = _abilityData[random.Next(0, _abilityData.Length)];
                
                if (CheckRepeatAbility(data))
                {
                    _abilityCount++;
                }
                else
                {
                    var ability = Instantiate(data.Ability, _abilityContainer);
                    _abilityCells.Add(ability);
                    _selectedTags.Add(data.Tag);
                }
                
            }
        }

        private bool CheckRepeatAbility(AbilData abilityData)
        {
            if (_selectedTags.Count == 0) return false;
            
            foreach (var tag in _selectedTags)
            {
                if (tag == abilityData.Tag)
                    return true;
            }
            
            return false;
        }

        public void Confirm()
        {
            if (_selectedAbility == null) return;
            
            _selectedAbility.Upgrade();
            
            foreach (var ability in _abilityCells)
                Destroy(ability.gameObject);
            
            _abilityCells.Clear();
            _selectedTags.Clear();
            _abilityCount = 3;

            GameSession.instance.UiIsActive = false;
            _window.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
