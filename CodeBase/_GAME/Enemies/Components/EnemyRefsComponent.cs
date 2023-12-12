using CodeBase._GAME.Common;
using UnityEngine;

namespace CodeBase._GAME.Enemies
{
    [System.Serializable]
    public struct EnemyRefsComponent
    {
        public EnemyAnimator EnemyAnimator;
        public Area FindTargetArea;
        public Rigidbody[] BloodBalls;
    }
}
