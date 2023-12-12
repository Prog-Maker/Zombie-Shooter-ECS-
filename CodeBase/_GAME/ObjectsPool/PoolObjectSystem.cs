using CodeBase._GAME.Pool;
using CodeBase.Types;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CodeBase._GAME.Systems.Player
{
    public class PoolObjectSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SpawnSignal>> _spawnSignals;
        private EcsFilterInject<Inc<PoolComponent>> _gamePools;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var spawnEntity in _spawnSignals.Value)
            {
                PoolableType sapwnType = _spawnSignals.Pools.Inc1.Get(spawnEntity).PoolableType;

                foreach (var poolEntity in _gamePools.Value)
                {
                    PoolableType poolType = _gamePools.Pools.Inc1.Get(poolEntity).PoolableType;

                    if(sapwnType == poolType)
                    {

                    }
                }
            }
        }
    }
}
