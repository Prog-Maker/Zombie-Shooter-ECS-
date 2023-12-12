using CodeBase.Components;
using CodeBase.Infrastrucure.Components;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Infrastructure.Views
{
    public class MonoEntity : MonoBehaviour
    {
        public EcsPackedEntity PackedEntity;
        public ViewComponent[] ViewComponents;
        
        private EcsWorld _world;
        [SerializeField]
        [ReadOnly]
        int _entity;

        private void Awake()
        {
            _world = ProjectContext.Container.EcsWorld;
            _entity = _world.NewEntity();
            PackedEntity = _world.PackEntity(_entity);

            ref var ec = ref _world.GetPool<EntityComponent>().Add(_entity);
            ec.Entity = _entity;

            ref var gc = ref _world.GetPool<GameObjectComponent>().Add(_entity);
            gc.GameObject = gameObject;

            ref var tc = ref _world.GetPool<TransformComponent>().Add(_entity);
            tc.Transform = transform;

            foreach (var viewComponent in ViewComponents)
            {
                viewComponent.Init(_entity, _world, this);
                viewComponent.AddComponents();
            }
        }

        public EcsWorld EcsWorld => _world;

        [Button]
        private void GetViewComponents()
        {
            ViewComponents = GetComponentsInChildren<ViewComponent>();
        }
    }
}
