using CodeBase._GAME.Components;
using CodeBase._GAME.Weapons;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CodeBase._GAME.Player
{
    public class PlayerWeaponIKSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PlayerRefsComponent, WeaponComponents>> _player;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _player.Value)
            {
                ref var playerRefs = ref _player.Pools.Inc1.Get(entity);
                var weaponData = _player.Pools.Inc2.Get(entity).WeaponData;

                playerRefs.RhandTarget.transform.position = weaponData.RhandIKPoint.position;
                playerRefs.RhandTarget.transform.rotation = weaponData.RhandIKPoint.rotation;

                playerRefs.LhandTarget.transform.position = weaponData.LhandIKPoint.position;
                playerRefs.LhandTarget.transform.rotation = weaponData.LhandIKPoint.rotation;
            }
        }
    }
}
