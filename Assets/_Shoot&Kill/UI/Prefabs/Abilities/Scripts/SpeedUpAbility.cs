using Assets.Prefabs.Characters.MainHero.Scripts;
using UnityEngine;

namespace Assets.UI.Prefabs.Abilities.Scripts
{
    public class SpeedUpAbility : AbilityCell
    {
        [SerializeField] private float _percent = 10f;
        private HeroMovement _movement;

        protected override void Awake()
        {
            base.Awake();
            _movement = HeroMovement.Instance;
        }
        
        public override void Upgrade()
        {
            _movement.UpSpeed(_percent);
        }

        protected override void SetText() => Text.text = $"Speed up: +{_percent}%";
    }
}
