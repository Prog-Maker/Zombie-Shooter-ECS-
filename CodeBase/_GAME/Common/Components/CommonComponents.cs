using UnityEngine;

namespace CodeBase._GAME.Common
{
    public struct AttackTag {}

    public struct HitTag 
    {
        public Vector3 Position;
        public Vector3 Normal;
    }

    public struct DeathTag
    {

    }

    public struct DestroyEntity { }
}
