using CodeBase.Infrastructure.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase._GAME.Ragdoll
{
    public class RagdollView : ViewComponent
    {
        public RagdollData RagdollData;
        public bool SetTriggersOnInit = false;

        public override void AddComponents()
        {
            ref var ragdoll = ref Add<RagdollComponent>();
            ragdoll.RagdollData = RagdollData;
        }

        [Button]
        private void CollectRagdoll()
        {
            RagdollData.Rigidbodies = GetComponentsInChildren<Rigidbody>();
            RagdollData.Colliders = GetComponentsInChildren<Collider>();

            foreach (var rb in RagdollData.Rigidbodies)
            {
                rb.isKinematic = true;
            }

            if (SetTriggersOnInit)
            {
                foreach (var col in RagdollData.Colliders)
                {
                    col.isTrigger = true;
                }
            }
        }

        [Button]
        private void ActivateColliders()
        {
            foreach (var col in RagdollData.Colliders)
            {
                col.enabled = true;
                col.isTrigger = false;
            }
        }
    }
}