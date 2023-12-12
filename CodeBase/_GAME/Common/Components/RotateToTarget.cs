using Mitfart.LeoECSLite.UnityIntegration.Attributes;
using UnityEngine;

namespace CodeBase._GAME.Components
{
    [System.Serializable]
    [GenerateView]
    public struct RotateToTarget
    {
        public Transform Target;
    }
}
