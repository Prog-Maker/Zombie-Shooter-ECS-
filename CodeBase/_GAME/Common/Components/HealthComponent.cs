using Mitfart.LeoECSLite.UnityIntegration.Attributes;

namespace CodeBase._GAME.Common
{
    [System.Serializable]
    [GenerateView]
    public struct HealthComponent
    {
        public float Current;
        public float MaxHealth;
    }
}
