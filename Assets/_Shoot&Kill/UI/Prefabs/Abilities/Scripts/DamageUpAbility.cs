using Assets.Prefabs.Guns.Scripts;
using UnityEngine;

namespace Assets.UI.Prefabs.Abilities.Scripts
{
    public class DamageUpAbility : AbilityCell
    {
        [SerializeField] private float _percent;
        [SerializeField] private DamageGunConfig _config;

        public override void Upgrade() => _config.ChangeMiniGunDamage(_percent);
        
        protected override void SetText() => Text.text = $"Damage up: +{_percent}%";


    }
}
