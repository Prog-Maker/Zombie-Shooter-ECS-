using CodeBase._GAME.Common;
using UnityEngine;

namespace CodeBase._GAME.Physics
{
    public struct OnTriggerEnterComponent
    {
        public Collider Other;
    }

    public struct OnCollisionEnterComponent
    {
        public Collision Other;
    }

    public struct OverlapComponent
    {
        public BoxSetting BoxSetting;
        public SphereSettings SphereSettings;
    }
}