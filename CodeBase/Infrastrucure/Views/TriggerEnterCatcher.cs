using CodeBase.Infrastructure.Views;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Infrastrucure.Views
{
    public class TriggerEnterCatcher : ViewComponent
    {
        [ReadOnly]
        public Collider Other;

        private void OnTriggerEnter(Collider other)
        {
            int entity = ProjectContext.Container.EcsWorld.NewEntity();
            ref var triggerEvent = ref ProjectContext.Container.EcsWorld.GetComponent<OnTriggerEnterEvent>(entity);
            triggerEvent.collider = other;
            triggerEvent.senderGameObject = gameObject;

            Other = other;
        }
    }
}