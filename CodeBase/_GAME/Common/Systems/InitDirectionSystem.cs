using CodeBase._GAME.Input;
using Leopotam.EcsLite;

namespace CodeBase._GAME.Common
{
    public class InitDirectionSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            systems.GetWorld().GetPool<DirectionComponent>().Add(systems.GetWorld().NewEntity());
        }
    }
}
