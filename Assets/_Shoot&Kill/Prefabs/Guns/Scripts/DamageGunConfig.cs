using System;
using UnityEngine;

namespace Assets.Prefabs.Guns.Scripts
{
    [CreateAssetMenu(fileName = "GunDamageConfig", menuName = "Configs/GunDamageConfig")]
    
    public class DamageGunConfig : ScriptableObject
    {
        public static Action onChange;
        
        public float MiniGunDamage = 10f;

        public void ChangeMiniGunDamage(float percent)
        {
            MiniGunDamage += MiniGunDamage / 100 * percent;
            
            onChange?.Invoke();
        }
    }
}
