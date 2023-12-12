using Mitfart.LeoECSLite.UnityIntegration.Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase._GAME.Common
{
    [System.Serializable]
    [GenerateView]
    public struct NavigationComponent
    {
        public NavMeshAgent NavMeshAgent;
        public Transform Target;
        public float MinDistance;
    }
}
