using CodeBase._GAME.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CodeBase._GAME.Common
{
    public class StopMoveByNavmeshSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<NavigationComponent, MoveForward, StopMoveByNavigationSignal>> _movers;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _movers.Value)
            {
                systems.GetWorld().DelComponent<MoveForward>(entity);

                ref var navComponent = ref  systems.GetWorld().GetComponent<NavigationComponent>(entity);
                navComponent.Target = null;
                navComponent.NavMeshAgent.isStopped = true;
            }
        }
    }
}
