using CodeBase._GAME.Common;
using CodeBase._GAME.Components;
using CodeBase._GAME.Enemies;
using CodeBase._GAME.Enemy;
using CodeBase.Infrastructure.Views;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase._GAME.Views.Enemies
{
    public class EnemyRefsView : ViewComponent
    {
        public NavMeshAgent NavMeshAgent;
        public Transform Target;
        public EnemyAnimator EnemyAnimator;
        public Area FindTargetArea;
        public Rigidbody[] BloodBall;
        public float MinDistance;

        public bool hasBlood = true;

        public override void AddComponents()
        {
            Add<EnemyTag>();

            if(hasBlood)
            {
                Add<BloodKeeper>();
            }

            ref var nav = ref Add<NavigationComponent>();
            nav.NavMeshAgent = NavMeshAgent;
            nav.Target = Target;
            nav.MinDistance = MinDistance;

            ref var refs = ref Add<EnemyRefsComponent>();
            refs.EnemyAnimator = EnemyAnimator;
            refs.FindTargetArea = FindTargetArea;
            refs.BloodBalls = BloodBall;
        }
    }
}