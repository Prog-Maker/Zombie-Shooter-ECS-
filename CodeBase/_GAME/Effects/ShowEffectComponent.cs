using UnityEngine;

namespace CodeBase._GAME.Effects
{
    [System.Serializable]
    public struct ShowEffectComponent
    {
        public EffectType EffectType;
        public Transform Parent;
        public Vector3 Position;
        public Vector3 Rotation;
    }

    public enum EffectType
    {
        None = 0,
        BulletSpark = 1,    
    }
}
