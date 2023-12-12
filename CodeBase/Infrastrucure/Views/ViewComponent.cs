using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Infrastructure.Views
{
    public class ViewComponent : MonoBehaviour
    {
        protected MonoEntity _monoEntity;
        protected int _entity;
        protected EcsWorld _ecsWorld;
        
        public MonoEntity MonoEntity => _monoEntity;

        public void Init(int entity, EcsWorld ecsWorld, MonoEntity monoEntity)
        {
            _entity = entity;
            _ecsWorld = ecsWorld;
            _monoEntity = monoEntity;
        }

        public virtual void AddComponents() { }

        protected ref T Add<T>() where T : struct
        {
            ref T comp = ref _ecsWorld.AddComponent<T>(_entity);
            return ref comp;
        }
    }
}
