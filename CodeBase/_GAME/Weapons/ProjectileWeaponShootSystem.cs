using CodeBase._GAME.Common;
using CodeBase.Components;
using CodeBase.Infrastructure.Views;
using CodeBase.Types;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Weapons
{
    public class ProjectileWeaponShootSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<WeaponComponents, ShootTag>> _weapons;
        private EcsCustomInject<ProjectContext> _context;

        private const float MaxDistance = 500f;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _weapons.Value)
            {
                var weaponData = _weapons.Pools.Inc1.Get(entity).WeaponData;

                if (weaponData != null && weaponData.WeaponType == WeaponDamageType.BulletSpawnWeapon)
                {
                    CreateBullet(systems, weaponData);
                    weaponData.ShootFeedbacks?.PlayFeedbacks();
                }
            }
        }

        private void CreateBullet(IEcsSystems systems, WeaponData weaponData)
        {
            BulletBase bullet = GetFromBulletsPool(weaponData);

            if (bullet.BulletData.MoveType == BulletMoveType.Forward)
            {
                if (BulletEntityIsAlive(systems, bullet, out int bulletEntity))
                    StartMoveForward(systems.GetWorld(), weaponData, bulletEntity);
            }
        }

        private bool BulletEntityIsAlive(IEcsSystems systems, BulletBase bullet, out int bulletEntity)
        {
            return bullet.MonoEntity.PackedEntity.Unpack(systems.GetWorld(), out bulletEntity);
        }

        private void StartMoveForward(EcsWorld world, WeaponData weaponData, int bulletEntity)
        {
            ref var bullet = ref world.GetComponent<BulletComponent>(bulletEntity);
            ref var speed = ref world.GetComponent<SpeedComponent>(bulletEntity);
            ref var destructTimer = ref world.AddComponent<DestructTimer>(bulletEntity);
            float flyDuration = 0;

            ResetBullet(ref bullet);
            FindTargetForBullet(weaponData, ref bullet, speed, ref flyDuration);
            InitSpeed(weaponData, ref speed);
            InitDestructTimer(weaponData, flyDuration, ref destructTimer);
            PrepareToMove(world, bulletEntity);
        }

        private void PrepareToMove(EcsWorld world, in int bulletEntity) 
                     => world.AddComponent<MoveForward>(bulletEntity);

        private void InitSpeed(WeaponData weaponData, ref SpeedComponent speed) 
                     => speed.Value = weaponData.BulletSpeed;

        private static void InitDestructTimer(WeaponData weaponData, in float flyDuration, ref DestructTimer destructTimer)
        {
            bool value = weaponData.FlyDuration <= flyDuration && flyDuration > 0f || flyDuration == 0f;
            destructTimer.Delay = value ? weaponData.FlyDuration : flyDuration;
            destructTimer.TimeLeft = destructTimer.Delay;
        }

        private void FindTargetForBullet(WeaponData weaponData, ref BulletComponent bullet, in SpeedComponent speed, ref float flyDuration)
        {
            var hit = RaycastForward(weaponData, ref bullet);

            if (hit != null)
            {
                TryGetEntity(hit.Value, ref bullet);
                bullet.TargetIsNotNull = true;
                bullet.TargetHitPoint = hit.Value.point;
                bullet.TargetHitNormal = hit.Value.normal;
                bullet.DistanceToTarget = hit.Value.distance;
                bullet.TargetCollider = hit.Value.collider;
                flyDuration = bullet.DistanceToTarget / speed.Value;
            }
        }

        private void ResetBullet(ref BulletComponent bullet)
        {
            bullet.TargetIsNotNull = false;
            bullet.TargetEntity = null;
            bullet.DistanceToTarget = 0f;
        }

        private RaycastHit? RaycastForward(WeaponData weaponData, ref BulletComponent bullet)
        {
            if (UnityEngine.Physics.Raycast(weaponData.ShootPoint.position,
                                            weaponData.ShootPoint.forward,
                                            out RaycastHit hit,
                                            MaxDistance,
                                            _context.Value.StaticDataService.GetLayers().BulletCollisionLayers,
                                            QueryTriggerInteraction.Ignore))
            {
                return hit;
            }
            else
            {
                return null;
            }
        }

        private void TryGetEntity(in RaycastHit hit, ref BulletComponent bullet)
        {
            var monoEntity = hit.collider.gameObject.GetComponentInParent<MonoEntity>();

            if (monoEntity)
            {
                bullet.TargetEntity = monoEntity.PackedEntity;
            }
        }

        private static BulletBase GetFromBulletsPool(WeaponData weaponData)
        {
            var bullet = weaponData.BulletsPool.GetBullet();
            bullet.transform.position = weaponData.ShootPoint.position;

            Quaternion spreadRotation = Quaternion.identity;

            if (weaponData.ScatterAngle > 0)
            {
                Vector3 bulletSpread = new Vector3(0, Random.Range(-weaponData.ScatterAngle, weaponData.ScatterAngle), 0);
                spreadRotation = Quaternion.Euler(bulletSpread);
            }
            bullet.transform.rotation = weaponData.ShootPoint.rotation * spreadRotation;
           
            bullet.gameObject.SetActive(true);
            return bullet;
        }
    }
}
