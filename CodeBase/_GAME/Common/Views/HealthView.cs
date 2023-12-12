using CodeBase._GAME.Common;
using CodeBase.Infrastructure.Views;

namespace CodeBase._GAME.Views
{
    public class HealthView : ViewComponent
    {
        public float Current;
        public float MaxHealth;

        public override void AddComponents()
        {
            Current = MaxHealth;
            ref var health = ref Add<HealthComponent>();
            health.MaxHealth = MaxHealth;
            health.Current = Current;
        }
    }
}