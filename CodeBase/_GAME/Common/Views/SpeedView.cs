using CodeBase.Components;
using CodeBase.Infrastructure.Views;

namespace CodeBase._GAME.Views
{
    public class SpeedView : ViewComponent
    {
        public float Speed;

        public override void AddComponents()
        {
            ref var speed = ref Add<SpeedComponent>();
            speed.Value = Speed;
        }
    }
}