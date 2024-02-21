using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace _Shoot_Kill.Prefabs.Characters.Enemies.Scripts.MotionVariety
{
    public class Approach : MotionBehavior
    {
        public Approach(NavMeshAgent agent, Transform target) : base(agent,target) {
        }

        public override void CalculatePath() {
            if(agent.pathStatus != NavMeshPathStatus.PathInvalid) agent.SetDestination(target.position);
        }
    }
}