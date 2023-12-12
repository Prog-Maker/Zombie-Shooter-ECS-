using CodeBase._GAME.Player;
using CodeBase.Infrastructure.Views;

namespace CodeBase._GAME.Views.Player
{
    public class UIAimView : ViewComponent
    {
        public override void AddComponents()
        {
            Add<UIReticleTag>();
        }
    }
}