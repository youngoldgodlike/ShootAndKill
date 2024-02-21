using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.UI.Prefabs.Abilities.Scripts
{
    public abstract class AbilityCell : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected TextMeshProUGUI Text;
        
        [SerializeField] private GameObject _defaultIcon;
        [SerializeField] private GameObject _selectedIcon;

        private AbilityWindowManager _abilityManager;

        protected virtual void Awake()
        {
            _abilityManager = AbilityWindowManager.Instance;
            SetText();
        } 
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Selected();
        }

        private void Selected()
        {
            _abilityManager.ChoiceAbility(() => Show(), this);
        }

        public void Hide()
        {
            _defaultIcon.SetActive(true);
            _selectedIcon.SetActive(false);
        }

        private void Show()
        {
            _defaultIcon.SetActive(false);
            _selectedIcon.SetActive(true);
        }

        public abstract void Upgrade();

        protected abstract void SetText();


    }
}
