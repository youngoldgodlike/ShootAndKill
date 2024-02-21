using Assets.Prefabs.Characters.MainHero.Scripts;
using UnityEngine;

namespace Assets.UI.Prefabs.Abilities.Scripts
{
    public class HpUpAbility : AbilityCell
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private float _hp;

        protected override void Awake()
        {
            base.Awake();
            _health = HeroHealth.Instance;
        }

        public override void Upgrade() => _health.UpHealth(_hp);

        protected override void SetText() => Text.text = $"Health up: +{_hp}";
    }
}
