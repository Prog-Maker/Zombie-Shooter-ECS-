using CodeBase._GAME.Input;
using CodeBase.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Player
{
    public class TopDownPlayerMovingSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PlayerTag, PlayerRefsComponent, SpeedComponent>> _player;
        private EcsFilterInject<Inc<DirectionComponent>> _inputDirection;

        private int _hor = Animator.StringToHash("Horizontal");
        private int _ver = Animator.StringToHash("Vertical");

        float _blendXLerp = 0f;
        float _blendZLerp = 0f;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (var playerEntity in _player.Value)
            {
                foreach (var inputEntity in _inputDirection.Value)
                {
                    ref var moveDirection = ref world.GetPool<DirectionComponent>().Get(inputEntity);
                    ref var playerRefs = ref world.GetPool<PlayerRefsComponent>().Get(playerEntity);
                    ref var speed = ref systems.GetWorld().GetPool<SpeedComponent>().Get(playerEntity);

                    Transform modelTransform = playerRefs.Model;
                    Animator animator = playerRefs.Animator;
                    CharacterController controller= playerRefs.CharacterController;

                    float blendX = Vector3.Dot(moveDirection.Value, modelTransform.right);
                    float blendZ = Vector3.Dot(moveDirection.Value, modelTransform.forward);

                    _blendXLerp = Mathf.Lerp(_blendXLerp, blendX, Time.deltaTime * speed.Value);
                    _blendZLerp = Mathf.Lerp(_blendZLerp, blendZ, Time.deltaTime * speed.Value);

                    animator.SetFloat(_hor, _blendXLerp);
                    animator.SetFloat(_ver, _blendZLerp);

                    moveDirection.Value.y = UnityEngine.Physics.gravity.y;

                    controller.Move(moveDirection.Value * speed.Value * Time.deltaTime);
                }
            }
        }
    }
}
