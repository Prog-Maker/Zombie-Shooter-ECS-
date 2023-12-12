using CodeBase._GAME.Effects;
using CodeBase.Infrastructure.Views;
using CodeBase.Types;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase._GAME.Weapons
{
    public class BulletBase : ViewComponent
    {
        public BulletData BulletData;

        public override void AddComponents()
        {
            if(!BulletData.GameObject) 
                BulletData.GameObject = gameObject;
            
            ref var projectile = ref Add<ProjectileTag>();
            projectile.ProjectileType = BulletData.Type;
            projectile.BulletMoveType = BulletData.MoveType;

            ref var bulletComp = ref Add<BulletComponent>();
            bulletComp.BulletData = BulletData;
        }
    }

    [System.Serializable]
    public class BulletData
    {
        public ProjectileType Type;
        public BulletMoveType MoveType;

        public EffectType SolidEffect;

        public EcsPackedEntity Entity;
        public bool EntityAttached = false;
        public float Speed;
        public float Damage;

        public ParticleSystem SolidImpactEffect;
        public GameObject GameObject;
    }
}