using CodeBase._GAME.Common;
using CodeBase._GAME.Input;
using CodeBase.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase._GAME.Player
{
    sealed class PlayerMoveByJoystickSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _player;
        private EcsFilter _input;
        private EcsWorld _world;

        private Vector3 _directionToMove;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _player = _world.Filter<PlayerTag>().Inc<TransformComponent>().Inc<SpeedComponent>().End();
            _input = _world.Filter<JoystickComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var inputEntity in _input)
            {
                ref var joystick = ref _world.GetPool<JoystickComponent>().Get(inputEntity).Joystick;

                if (joystick != null)
                {
                    _directionToMove = joystick.Direction;
                    _directionToMove = new Vector3(_directionToMove.x, 0, _directionToMove.y);
                    _directionToMove.Normalize();

                    foreach (var playerEnitiy in _player)
                    {
                        ref var playerTransform = ref _world.GetPool<TransformComponent>().Get(playerEnitiy).Transform;
                        ref var speed = ref _world.GetPool<SpeedComponent>().Get(playerEnitiy).Value;

                        var movingPool = _world.GetPool<MoveForward>();
                        
                        if (_directionToMove != Vector3.zero)
                        {
                            if(!movingPool.Has(playerEnitiy)) 
                                movingPool.Add(playerEnitiy);
                           
                            Move(playerTransform, speed, _directionToMove);
                        }
                        else
                        {
                            if (movingPool.Has(playerEnitiy))
                                movingPool.Del(playerEnitiy);
                        }
                    }
                }
            }
        }

        private void Move(Transform playerTransform, float speed, Vector3 directionToMove)
        {
            playerTransform.Translate(directionToMove * speed * Time.deltaTime, Space.World);
        }
    }
}