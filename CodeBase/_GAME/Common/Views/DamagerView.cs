using CodeBase.Infrastructure.Views;

namespace CodeBase._GAME.Common.Views
{
    public class DamagerView : ViewComponent
    {
        public float DamagaValue;

        public override void AddComponents()
        {
            ref var damage = ref Add<DamagerComponent>();
            damage.DamageValue = DamagaValue;
        }
    }
}