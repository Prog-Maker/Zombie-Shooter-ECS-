using CodeBase._GAME.Weapons;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Player
{
    public class PlayerShootByMouseSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PlayerTag, WeaponComponents>> _player;

        private EcsPool<ShootTag> _shootPool;

        public PlayerShootByMouseSystem(EcsWorld world)
        {
            _shootPool = world.GetPool<ShootTag>();
        }

        private float _timer;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _player.Value)
            {
                ref WeaponComponents weapon = ref _player.Pools.Inc2.Get(entity);

                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    Shoot(entity, systems);
                }

                if (UnityEngine.Input.GetMouseButton(0))
                {
                    if (_timer > Time.time) return;

                    _timer = Time.time + weapon.WeaponData.FireRate;

                    Shoot(entity, systems);
                }
            }
        }

        private void Shoot(int entity, IEcsSystems systems)
        {
            _shootPool.AddComponent(entity);
        }
    }
}
