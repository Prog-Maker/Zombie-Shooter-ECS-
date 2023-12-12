using CodeBase.Infrastructure.Views;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Common
{
    public class DamageByTriggersSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnTriggerEnterEvent>> _triggeredObjects;

        public void Run(IEcsSystems systems)
        {
            foreach (var entityTriggerEvent in _triggeredObjects.Value)
            {
                ref var triggeredEvent = ref systems.GetWorld().GetComponent<OnTriggerEnterEvent>(entityTriggerEvent);
                GameObject gameObject = triggeredEvent.senderGameObject;
                Collider collider = triggeredEvent.collider;

                HealthComponent healthComponent = default;
                bool healthEnables = TryGetComponent<HealthComponent>(systems, collider.gameObject, ref healthComponent);

                DamagerComponent damageComponent = default;
                bool damageEnables = TryGetComponent<DamagerComponent>(systems, gameObject, ref damageComponent);

                if(healthEnables && damageEnables)
                {
                    healthComponent.Current -= damageComponent.DamageValue;
                }
            }
        }

        private bool TryGetComponent<T>(IEcsSystems systems, GameObject gameObject, ref T component) where T : struct
        {
            var monoEntity = gameObject.GetComponentInParent<MonoEntity>();

            if (monoEntity && monoEntity.PackedEntity.Unpack(systems.GetWorld(), out var entity))
            {
                if (systems.GetWorld().GetPool<T>().Has(entity))
                {
                    component = ref systems.GetWorld().GetComponent<T>(entity);
                    return true;
                }
            }

            component = default(T);
            return false;
        }
    }
}
