using CodeBase._GAME.Common;
using CodeBase._GAME.Enemy;
using CodeBase._GAME.Ragdoll;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CodeBase._GAME.Enemies
{
    public class EnemyDeathSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<EnemyTag, EnemyRefsComponent, DeathTag>> _damagedEnemies;
        private EcsPoolInject<StopMoveByNavigationSignal> _stopPool;
        private EcsPoolInject<ActivateRagdollSignal> _ragdollPool;
        private EcsPoolInject<DestroyEntity> _destroyEntityPool;

        public void Run(IEcsSystems systems)
        {
            foreach (int enemyEntity in _damagedEnemies.Value)
            {
                _stopPool.Value.Add(enemyEntity);
                _ragdollPool.Value.Add(enemyEntity);
                _destroyEntityPool.Value.Add(enemyEntity);
            }
        }
    }
}
