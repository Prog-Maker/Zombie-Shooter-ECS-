using CodeBase._GAME.Components;
using CodeBase.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase._GAME.Common
{
    sealed class MoveTransfromForwardSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entities = world.Filter<TransformComponent>()
                                .Inc<SpeedComponent>()
                                .Inc<MoveForward>()
                                .Exc<NavigationComponent>()
                                .End();

            foreach (var entity in entities) 
            {
                ref var transform = ref world.GetPool<TransformComponent>().Get(entity).Transform;
                ref var speed = ref world.GetPool<SpeedComponent>().Get(entity).Value;

                transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            }
        }
    }
}