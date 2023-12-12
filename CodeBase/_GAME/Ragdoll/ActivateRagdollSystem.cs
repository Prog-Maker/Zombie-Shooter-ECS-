using CodeBase._GAME.Common;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Ragdoll
{
    public class ActivateRagdollSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<RagdollComponent, ActivateRagdollSignal, AnimatorComponent>> _ragdolls;

        public void Run(IEcsSystems systems)
        {
            foreach (var ragdollEntity in _ragdolls.Value)
            {
                ref var signal = ref systems.GetWorld().GetComponent<ActivateRagdollSignal>(ragdollEntity);
                RagdollData ragdollData = systems.GetWorld().GetComponent<RagdollComponent>(ragdollEntity).RagdollData;
                Animator animator = systems.GetWorld().GetComponent<AnimatorComponent>(ragdollEntity).Animator;

                animator.enabled = false;

                foreach (var collider in ragdollData.Colliders)
                {
                    collider.isTrigger = false;
                }

                foreach (var rigidbody in ragdollData.Rigidbodies)
                {
                    rigidbody.isKinematic = false;
                    rigidbody.AddForce(signal.Direction * signal.Force, signal.ForceMode);
                }
            }
        }
    }
}
