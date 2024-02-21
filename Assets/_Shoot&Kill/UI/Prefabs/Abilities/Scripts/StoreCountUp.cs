using Assets.Prefabs.Characters.MainHero.Scripts;
using UnityEngine;

namespace Assets.UI.Prefabs.Abilities.Scripts
{
    public class StoreCountUp : AbilityCell
    {
        [SerializeField] private int _value = 5;
        private HeroAttack _attack;

        protected override void Awake()
        {
            base.Awake();
            _attack = HeroAttack.Instance;
        }
        
        public override void Upgrade()
        {
            var gun = _attack.GetGun();
            gun.SetStoreCount(_value);
        }

        protected override void SetText() => Text.text = $"Store count: +{_value}";
    }
}