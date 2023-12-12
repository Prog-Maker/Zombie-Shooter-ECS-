using Mitfart.LeoECSLite.UnityIntegration.Attributes;
using UnityEngine;

namespace CodeBase._GAME.Player
{
    [System.Serializable]
    [GenerateView]
    public struct PlayerRefsComponent
    {
        public Transform Model;
        public CharacterController CharacterController;
        public Animator Animator;

        public Transform RhandTarget;
        public Transform LhandTarget;

        public GameObject Light;
    }
}
