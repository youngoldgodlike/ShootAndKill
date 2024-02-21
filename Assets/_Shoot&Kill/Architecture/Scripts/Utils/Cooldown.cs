using System;
using UnityEngine;

namespace Assets.Architecture.Scripts.Utils
{
    [Serializable]
    public class Cooldown
    {
        public float Delay;
        private float _timesUp;
        
        public bool IsReady => _timesUp <= Time.time;

        public void Reset() => _timesUp = Time.time + Delay;


    }
}
