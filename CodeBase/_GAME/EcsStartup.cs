using CodeBase._GAME.Common;
using CodeBase._GAME.Components;
using CodeBase._GAME.Data;
using CodeBase._GAME.Effects;
using CodeBase._GAME.Enemies;
using CodeBase._GAME.Input;
using CodeBase._GAME.Player;
using CodeBase._GAME.Ragdoll;
using CodeBase._GAME.Weapons;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField]
        public LayersData Layers;
        
        EcsWorld _world;
        IEcsSystems _systems;
#if UNITY_EDITOR
        IEcsSystems _editorSystems;
#endif

        void Awake()
        {
            _world = new EcsWorld();
            ProjectContext.Container.SetWorld(_world);

            EcsPhysicsEvents.ecsWorld = _world;

            _systems = new EcsSystems(_world)

            .Add(new InitDirectionSystem())
            .Add(new KeyboardSetDirectionSystem())
            .Add(new UIReticleMoveSystem())
            
            .Add(new PlayerLightControlSystem())
            .Add(new PlayerLookAtReticleSystem ())
            .Add(new TopDownPlayerMovingSystem())
            .Add(new PlayerWeaponIKSystem())
            .Add(new PlayerShootByMouseSystem(_world))
            
            .Add(new EnemyFindTargetSystem())
            
            .Add(new ProjectileWeaponShootSystem())
            .Add(new BulletsCheckCollisionSystem())
            .Add(new HideBulletByTimeSystem(_world))

            .Add(new MoveTransfromForwardSystem())
            
            .Add(new TakeDamageSystem())
            .Add(new EnemyDeathSystem())
            
            .Add(new StopMoveByNavmeshSystem())
            .Add(new MoveByNavmeshSystem())
            
            .Add(new EnemyAnimationSystem())
            .Add(new SpawnEffectSystem())
            .Add(new SpawnBloodBallSystem())
            .Add(new ActivateRagdollSystem())

            .Add(new DestroyEntitySystem())

            .DelHere<DeathTag>()
            .DelHere<HitTag>()
            .DelHere<Damage>()
            .DelHere<ShootTag>()
            .DelHere<ShowEffectComponent>()
            .DelHere<ActivateRagdollSignal>()
            .DelHere<StopMoveByNavigationSignal>()
            .DelHerePhysics()
            ;
#if UNITY_EDITOR
            // Создаем отдельную группу для отладочных систем.
            _editorSystems = new EcsSystems(_systems.GetWorld());
            _editorSystems
              .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
              .Add(new Mitfart.LeoECSLite.UnityIntegration.EcsWorldDebugSystem())
              ;

            _editorSystems.Inject();
            _editorSystems.Init();
#endif
            _systems.Inject(ProjectContext.Container);
            _systems.Inject(Layers);
            _systems.Inject();
            _systems.Init();
        }

        void Update()
        {
            // process systems here.
            _systems?.Run();
#if UNITY_EDITOR
            // Выполняем обновление состояния отладочных систем. 
            _editorSystems?.Run();
#endif
        }

        void OnDestroy()
        {
#if UNITY_EDITOR
            // Выполняем очистку отладочных систем.
            if (_editorSystems != null)
            {
                _editorSystems.Destroy();
                _editorSystems = null;
            }
#endif

            if (_systems != null)
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy();
                _systems = null;
            }

            // cleanup custom worlds here.

            // cleanup default world.
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}