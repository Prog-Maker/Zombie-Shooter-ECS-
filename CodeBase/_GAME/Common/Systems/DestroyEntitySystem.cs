using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CodeBase._GAME.Common
{
    public class DestroyEntitySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DestroyEntity>> _forDestroy;

        public void Run(IEcsSystems systems)
        {
            foreach (var entityForDestory in _forDestroy.Value) 
            {
                systems.GetWorld().DelEntity(entityForDestory);
            }
        }
    }
}
