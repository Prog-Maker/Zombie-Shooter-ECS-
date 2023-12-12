using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase._GAME.Input
{
    public class KeyboardSetDirectionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DirectionComponent>> _direction;

        private const string _horizontal = "Horizontal";
        private const string _vertical = "Player1_Vertical";

        public void Run(IEcsSystems systems)
        {
            var dirX = UnityEngine.Input.GetAxisRaw(_horizontal);
            var dirY = UnityEngine.Input.GetAxisRaw(_vertical);

            foreach (var entity in _direction.Value)
            {
                ref var dir = ref systems.GetWorld().GetPool<DirectionComponent>().Get(entity);
                dir.Value = new Vector3(dirX, 0, dirY);
            }
        }

        private static bool IsZeroDirection(float dirX, float dirY)
        {
            return math.abs(dirX) < 0.01f && math.abs(dirY) < 0.01f;
        }
    }
}