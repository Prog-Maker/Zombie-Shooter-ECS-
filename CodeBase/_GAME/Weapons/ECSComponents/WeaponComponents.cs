using CodeBase.Types;
using Leopotam.EcsLite;
using Mitfart.LeoECSLite.UnityIntegration.Attributes;
using UnityEngine;

namespace CodeBase._GAME.Weapons
{
    [System.Serializable]
    [GenerateView]
    public struct WeaponComponents
    {
        public WeaponData WeaponData;
    }

    public struct ShootTag { }

    [System.Serializable]
    public struct BulletComponent
    {
        public BulletData BulletData;
        public bool TargetIsNotNull;
        public Vector3 TargetHitPoint;
        public Vector3 TargetHitNormal;
        public float DistanceToTarget;

        public EcsPackedEntity? TargetEntity;
        public Collider TargetCollider;
    }

    [System.Serializable]
    public struct ProjectileTag
    {
        public ProjectileType ProjectileType;
        public BulletMoveType BulletMoveType;
    }
}
