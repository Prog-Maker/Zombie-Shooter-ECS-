using CodeBase._GAME.Common;
using CodeBase._GAME.Enemy;
using CodeBase._GAME.Player;
using CodeBase.Infrastrucure.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase._GAME.Enemies
{
    sealed class EnemyFindTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _player;
        private EcsFilter _enemies;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _player = _world.Filter<PlayerTag>().Inc<TransformComponent>().Inc<GameObjectComponent>().End();
            _enemies = _world.Filter<EnemyTag>().Inc<EnemyRefsComponent>().Inc<NavigationComponent>().Exc<MoveForward>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var enemyEntity in _enemies)
            {
                ref var nav = ref _world.GetComponent<NavigationComponent>(enemyEntity);
                ref var enemyRefs = ref _world.GetComponent<EnemyRefsComponent>(enemyEntity);
                Transform enemyTransform = _world.GetComponent<TransformComponent>(enemyEntity).Transform;

                foreach (var playerEntity in _player)
                {
                    var playerTransform = _world.GetPool<TransformComponent>().Get(playerEntity).Transform;

                    if (playerTransform != null && TargetInArea(playerTransform, enemyTransform, enemyRefs.FindTargetArea))
                    {
                        nav.Target = playerTransform;
                        _world.AddComponent<MoveForward>(enemyEntity);
                    }
                }
            }
        }

        private bool TargetInArea(Transform playerTransform, Transform enemyTransform, Area area)
        {
            var distance = area.SphereSettings.Radius * area.SphereSettings.Radius;
            return (playerTransform.position - enemyTransform.position).sqrMagnitude <= distance;
        }
    }
}