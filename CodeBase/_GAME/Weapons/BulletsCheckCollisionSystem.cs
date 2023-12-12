using CodeBase._GAME.Common;
using CodeBase._GAME.Components;
using CodeBase._GAME.Effects;
using CodeBase.Infrastrucure;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Weapons
{
    public class BulletsCheckCollisionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<ProjectileTag, BulletComponent, TransformComponent, DamagerComponent, MoveForward>> _movingBullets;
        private EcsPoolInject<Damage> _damagedPool;
        private EcsPoolInject<HealthComponent> _healthsPool;
        private EcsPoolInject<DamagerComponent> _damagersPool;

        private EcsCustomInject<ProjectContext> _context;

        private float _minDistance = 0.5f;

        public void Run(IEcsSystems systems)
        {
            foreach (var bulletEntity in _movingBullets.Value)
            {
                ref var bullet = ref systems.GetWorld().GetComponent<BulletComponent>(bulletEntity);
                ref var transformComponent = ref systems.GetWorld().GetComponent<TransformComponent>(bulletEntity);

                if (bullet.TargetIsNotNull && DistanceReached(bullet, transformComponent))
                {
                    DamageTarget(systems, bulletEntity, ref bullet);
                    ShowEffect(systems, ref bullet);
                    Destruct(systems, bulletEntity);
                }
            }
        }

        private static void Destruct(IEcsSystems systems, int bulletEntity)
        {
            ref var timer = ref systems.GetWorld().GetComponent<DestructTimer>(bulletEntity);
            timer.TimeLeft = 0;
        }

        private void DamageTarget(IEcsSystems systems, int bulletEntity, ref BulletComponent bullet)
        {
            if (bullet.TargetEntity.Value.Unpack(systems.GetWorld(), out int entity))
            {
                if (entity > 0 && _healthsPool.Value.Has(entity) && !_damagedPool.Value.Has(entity))
                {
                    ref var damage = ref _damagedPool.Value.Add(entity);
                    damage.Value = _damagersPool.Value.Get(bulletEntity).DamageValue;
                    damage.Position = bullet.TargetHitPoint;
                    damage.Normal = bullet.TargetHitNormal;
                }
            }
        }

        private bool DistanceReached(BulletComponent bullet, TransformComponent transformComponent)
        {
            float sqrMagnitude = (bullet.TargetHitPoint - transformComponent.Transform.position).sqrMagnitude;
            return sqrMagnitude <= (_minDistance * _minDistance);
        }

        private void ShowEffect(IEcsSystems systems, ref BulletComponent bullet)
        {
            if (bullet.TargetCollider && bullet.TargetCollider.IsLayer(_context.Value.StaticDataService.GetLayers().SolidBodyLayers))
            {
                int entity = systems.GetWorld().NewEntity();
                ref var show = ref systems.GetWorld().GetComponent<ShowEffectComponent>(entity);
                show.EffectType = bullet.BulletData.SolidEffect;
                show.Parent = null;
                show.Position = bullet.TargetHitPoint;
                show.Rotation = Quaternion.LookRotation(bullet.TargetHitNormal).eulerAngles;
            }
        }
    }
}
