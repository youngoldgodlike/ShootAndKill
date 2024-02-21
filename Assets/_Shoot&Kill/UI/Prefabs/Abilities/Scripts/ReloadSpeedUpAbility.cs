using Assets.Prefabs.Characters.MainHero.Scripts;
using UnityEngine;

namespace Assets.UI.Prefabs.Abilities.Scripts
{
    public class ReloadSpeedUpAbility : AbilityCell
    {
        [SerializeField] private float _percent = 10f;
        private HeroAttack _attack;
        
        protected override void Awake()
        {
            base.Awake();
            _attack = HeroAttack.Instance;
        }
        
        public override void Upgrade()
        {
            var gun = _attack.GetGun();
            gun.SetTimeReload(_percent);
        }

        protected override void SetText() => Text.text = $"Reload speed: -{_percent}%";
    }
}