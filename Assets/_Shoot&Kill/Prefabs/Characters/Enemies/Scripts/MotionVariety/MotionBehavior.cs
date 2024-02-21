using UnityEngine;
using UnityEngine.AI;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.MotionVariety
{
    public abstract class MotionBehavior
    {
        protected NavMeshAgent agent;
        protected Transform target;
        
        public MotionBehavior(NavMeshAgent agent,Transform target) {
            this.agent = agent;
            this.target = target;
        }
        
        public abstract void CalculatePath();
    }
}