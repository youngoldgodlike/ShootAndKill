using Assets.Prefabs.Characters.MainHero.Scripts;

namespace Assets.UI.Prefabs.Abilities.Scripts
{
    public class DashCooldownAbility : AbilityCell
    {
        private float _percent = 5f;
        private HeroMovement _hero;

        protected override void Awake()
        {
            base.Awake();
            _hero = FindObjectOfType<HeroMovement>();
        }

        public override void Upgrade() => _hero.UpDashDelay(_percent);

        protected override void SetText() => Text.text = $"Dash cooldown: -{_percent}%";

    }
}