using UnityEngine;
using UnityEngine.AI;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.MotionVariety
{
    public class KeepDistance : MotionBehavior
    {
        public KeepDistance(NavMeshAgent agent, Transform target) : base(agent, target) {
        }

        public override void CalculatePath() {
        }
    }
}