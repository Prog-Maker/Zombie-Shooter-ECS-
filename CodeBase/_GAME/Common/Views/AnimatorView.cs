using CodeBase._GAME.Components;
using CodeBase.Infrastructure.Views;
using UnityEngine;

namespace CodeBase._GAME.Common.Views
{
    public class AnimatorView : ViewComponent
    {
        public Animator Animator;

        public override void AddComponents()
        {
            Add<AnimatorComponent>().Animator = Animator;
        }

    }
}