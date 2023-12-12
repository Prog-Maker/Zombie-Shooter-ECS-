using CodeBase._GAME.Input;
using CodeBase.Infrastructure.Views;

namespace CodeBase._GAME.Views.Input
{
    public class JoysticView : ViewComponent
    {
        public Joystick Joystick;
        public override void AddComponents()
        {
            ref var joy = ref Add<JoystickComponent>();
            joy.Joystick = Joystick;
        }
    }
}