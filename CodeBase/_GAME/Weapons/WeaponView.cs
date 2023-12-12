using CodeBase._GAME.Components;
using CodeBase.Infrastructure.Views;
using CodeBase.Types;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace CodeBase._GAME.Weapons
{
    public class WeaponView : ViewComponent
    {
        public WeaponData WeaponData;
        public override void AddComponents()
        {
            ref var weaponComponent = ref Add<WeaponComponents>();
            weaponComponent.WeaponData = WeaponData;

            WeaponData.BulletsPool.Init(WeaponData.Parent.root.parent);
        }
    }

    [System.Serializable]
    public class WeaponData
    {
        public WeaponDamageType WeaponType;
        public float FireRate = 0.1f;
        public float BulletSpeed = 10.0f;
        public float ScatterAngle = 0f;
        public float Damage = 10f;
        public float FlyDuration;

        public Transform Parent;
        public Transform Model;
        public Transform ShootPoint;
        
        public MMFeedbacks ShootFeedbacks;

        public BulletsPool BulletsPool;

        public Transform RhandIKPoint;
        public Transform LhandIKPoint;
    }
}