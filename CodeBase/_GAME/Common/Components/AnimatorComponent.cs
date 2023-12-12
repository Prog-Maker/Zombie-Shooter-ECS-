using Mitfart.LeoECSLite.UnityIntegration.Attributes;
using UnityEngine;

namespace CodeBase._GAME.Common
{
    [System.Serializable]
    [GenerateView]
    public struct AnimatorComponent
    {
        public Animator Animator;
    }
}
