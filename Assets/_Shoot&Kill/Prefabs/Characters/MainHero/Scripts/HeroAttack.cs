using _Shoot_Kill.Architecture.Scripts;
using _Shoot_Kill.UI.Prefabs.PauseMenu.Scripts;
using Assets.Prefabs.Guns.Scripts;
using Assets.UI.Prefabs.Abilities.Scripts;
using UnityEngine;

namespace Assets.Prefabs.Characters.MainHero.Scripts
{
    public class HeroAttack : Singleton<HeroAttack>
    {
        [SerializeField] private ProjectileAttack _gun;

        private void Update()
        {
            if (GameSession.instance.UiIsActive) return;
           
            if (Input.GetMouseButton(0))
                _gun.PerformAttack();
        }

        public ProjectileAttack GetGun() => _gun;
        
    }
}
