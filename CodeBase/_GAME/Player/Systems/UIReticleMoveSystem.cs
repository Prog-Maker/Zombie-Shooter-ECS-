using CodeBase._GAME.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using static UnityEngine.Input;

namespace CodeBase._GAME.Player
{
    public class UIReticleMoveSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<UIReticleTag, RectTransfromComponent>> _uIAim;

        private Camera _cam = Camera.main;

        public void PreInit(IEcsSystems systems)
        {
            Cursor.visible = false;
        }

        public void Run(IEcsSystems systems)
        {
            if(Cursor.visible) Cursor.visible = false;

            foreach (var entity in _uIAim.Value)
            {
                var pos = new Vector3(mousePosition.x, mousePosition.y, _cam.nearClipPlane);
                ref var rc = ref systems.GetWorld().GetPool<RectTransfromComponent>().Get(entity);

                rc.Value.position = pos;
            }
        }
    }
}
