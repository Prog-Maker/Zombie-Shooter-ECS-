using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CodeBase._GAME.Player
{
    public class PlayerLightControlSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PlayerRefsComponent>> _player;

        public void Run(IEcsSystems systems)
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F))
            {
                foreach (var entity in _player.Value)
                {
                    ref var playerRefs = ref systems.GetWorld().GetComponent<PlayerRefsComponent>(entity);

                    playerRefs.Light.SetActive(!playerRefs.Light.activeSelf);
                }
            }
        }
    }
}
