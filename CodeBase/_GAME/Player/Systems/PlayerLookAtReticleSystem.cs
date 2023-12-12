using CodeBase._GAME.Weapons;
using CodeBase.Infrastrucure;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Player
{
    public class PlayerLookAtReticleSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<PlayerTag, PlayerRefsComponent>> _player;
        private EcsCustomInject<ProjectContext> _context;

        private const float _rayLength = 100f;

        private LayerMask _checkLayerMask;

        private Camera _camera = Camera.main;

        public void Init(IEcsSystems systems)
        {
            _checkLayerMask = _context.Value.StaticDataService.GetLayers().PlayerRotateLayers;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _player.Value)
            {
                Transform modelTransform = systems.GetWorld().GetComponent<PlayerRefsComponent>(playerEntity).Model;
                WeaponData weapon = systems.GetWorld().GetComponent<WeaponComponents>(playerEntity).WeaponData;

                var mousePos = UnityEngine.Input.mousePosition;
                mousePos.z = _camera.transform.position.y - modelTransform.position.y;
                Vector3 lookPos = _camera.ScreenToWorldPoint(mousePos);
                Vector3 lookDirection = lookPos - modelTransform.position;
                lookDirection.y = 0f;
                modelTransform.rotation = Quaternion.LookRotation(lookDirection);

                RotateWeapon(weapon, mousePos);
            }
        }

        private void RotateWeapon(WeaponData weapon, Vector3 mousePos)
        {
            var ray = _camera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, _camera.nearClipPlane));

            if (UnityEngine.Physics.Raycast(ray, out var hit, _rayLength, _checkLayerMask))
            {
                Vector3 rayDirection = (ray.origin - hit.point).normalized;
                float angleRadians = Mathf.Deg2Rad * (90f - Vector3.Angle(hit.normal, rayDirection));
                float height = weapon.Parent.position.y; // GetHeight(weapon, hit.point);
                float vectorSize = height / Mathf.Sin(angleRadians);
                Vector3 vector = rayDirection * vectorSize;
                Vector3 point = hit.point + vector;

                var direction = point - weapon.Parent.position;
                weapon.Parent.rotation = Quaternion.LookRotation(direction);
                weapon.Parent.localRotation = GetRotation(weapon.Parent.localRotation);
            }
        }

        private Quaternion GetRotation(Quaternion localRotation)
        {
            var rotationAngles = localRotation.eulerAngles;
            rotationAngles.y = GameExtensions.ClampAngle(rotationAngles.y, -90f, 90f);
            return Quaternion.Euler(rotationAngles);
        }

        private float GetHeight(WeaponData weapon, Vector3 hitPoint)
        {
            float weaponYpos = weapon.Parent.position.y;
            float height = hitPoint.y <=  weaponYpos ? hitPoint.y : weaponYpos;
            return Mathf.Clamp(height, 0f, weaponYpos);
        }
    }
}
