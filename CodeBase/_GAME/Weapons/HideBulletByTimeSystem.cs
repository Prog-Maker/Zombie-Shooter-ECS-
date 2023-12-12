using CodeBase._GAME.Common;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Weapons
{
    public class HideBulletByTimeSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DestructTimer, ProjectileTag, TransformComponent>> _bullets;
        private EcsWorld _world;

        public HideBulletByTimeSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bullets.Value)
            {
                ref var destructTimer = ref _world.GetComponent<DestructTimer>(entity);
                var transform = _world.GetComponent<TransformComponent>(entity).Transform;

                if (destructTimer.TimeLeft > 0)
                {
                    destructTimer.TimeLeft -= Time.deltaTime;
                }
                else
                {
                    _world.DelComponent<MoveForward>(entity);
                    _world.DelComponent<DestructTimer>(entity);
                    transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
