using CodeBase._GAME.Components;
using CodeBase.Components;
using Leopotam.EcsLite;

namespace CodeBase._GAME.Common
{
    sealed class MoveByNavmeshSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsPool<NavigationComponent> _navPool;
        private EcsFilter _filter;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _navPool = _world.GetPool<NavigationComponent>();
            _filter = _world.Filter<NavigationComponent>().Inc<SpeedComponent>().Inc<MoveForward>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var nav = ref _navPool.Get(entity); 
                ref var speed = ref _world.GetPool<SpeedComponent>().Get(entity);
                
                if (nav.Target != null && IsTargetNotReached(ref nav) && nav.NavMeshAgent.isOnNavMesh)
                {
                    nav.NavMeshAgent.speed = speed.Value;
                    nav.NavMeshAgent.SetDestination(nav.Target.position);
                }
            }
        }

        private bool IsTargetNotReached(ref NavigationComponent navigationComponent)
        {
            float min = navigationComponent.MinDistance * navigationComponent.MinDistance;
            float sqrDist = (navigationComponent.NavMeshAgent.transform.position - navigationComponent.Target.position).sqrMagnitude;

            return sqrDist >= min;
        }
    }
}