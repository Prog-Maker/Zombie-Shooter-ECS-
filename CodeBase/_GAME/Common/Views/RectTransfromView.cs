using CodeBase._GAME.Components;
using CodeBase.Infrastructure.Views;
using UnityEngine;

namespace CodeBase._GAME.Views
{
    public class RectTransfromView : ViewComponent
    {
        public RectTransform RectTransform;

        public override void AddComponents()
        {
            ref var rc = ref Add<RectTransfromComponent>();
            rc.Value = RectTransform;
        }
    }
}