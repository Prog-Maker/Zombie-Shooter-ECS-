using CodeBase._GAME.Common;
using CodeBase.Infrastructure.Views;
using UnityEngine;

namespace CodeBase._GAME.Player
{
    public class PlayerRefsView : ViewComponent
    {
        public Animator Animator;
        public CharacterController CharacterController;
        public Transform Model;

        public Transform RhandTarget;
        public Transform LhandTarget;

        public GameObject Light;

        public override void AddComponents()
        {
            Add<PlayerTag>();
            
            ref var ac = ref Add<AnimatorComponent>();
            ac.Animator = Animator;

            ref var prefs = ref Add<PlayerRefsComponent>();
            prefs.Model= Model;
            prefs.CharacterController = CharacterController;
            prefs.Animator = Animator;
            prefs.RhandTarget = RhandTarget;
            prefs.LhandTarget = LhandTarget;
            prefs.Light = Light;
        }
    }
}