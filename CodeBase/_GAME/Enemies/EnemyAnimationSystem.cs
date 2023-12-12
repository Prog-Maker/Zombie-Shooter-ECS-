using CodeBase._GAME.Common;
using CodeBase._GAME.Enemy;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Enemies
{
    public class EnemyAnimationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<EnemyRefsComponent, EnemyTag, AnimatorComponent>> _enemies;
        private EcsPoolInject<MoveForward> _movePool;
        private EcsPoolInject<AttackTag> _attackPool;
        private EcsPoolInject<HitTag> _hitsPool;
        private EcsPoolInject<ActionTimer> _timers;

        public void Run(IEcsSystems systems)
        {
            foreach (var enemyEntity in _enemies.Value)
            {
                ref var enemyRefs = ref systems.GetWorld().GetComponent<EnemyRefsComponent>(enemyEntity);
                EnemyAnimator enemyAnimator = enemyRefs.EnemyAnimator;
                
                TryOffHitLayer(enemyEntity, enemyAnimator);

                PlayLocomotion(enemyEntity, enemyAnimator);
                PlayAttack(enemyEntity, enemyAnimator);
                PlayOnHit(enemyEntity, enemyAnimator);
            }
        }

        private void TryOffHitLayer(int enemyEntity, EnemyAnimator enemyAnimator)
        {
            if (_timers.Value.Has(enemyEntity))
            {
                ref var actionTimer = ref _timers.Value.Get(enemyEntity);

                if (actionTimer.Current > 0)
                {
                    actionTimer.Current -= Time.deltaTime;
                }
                else
                {
                    _timers.Value.Del(enemyEntity);
                    enemyAnimator.OffHitLayer();
                }
            }
        }

        private void PlayOnHit(int enemyEntity, EnemyAnimator enemyAnimator)
        {
            if (_hitsPool.Value.Has(enemyEntity))
                enemyAnimator.PlayOnHit();
        }

        private void PlayAttack(int enemyEntity, EnemyAnimator enemyAnimator)
        {
            enemyAnimator.PlayOnAttack(_attackPool.Value.Has(enemyEntity));
        }

        private void PlayLocomotion(int enemyEntity, EnemyAnimator enemyAnimator)
        {
            if (_movePool.Value.Has(enemyEntity))
            {
                enemyAnimator.PlayWalk();
            }
            else
            {
                enemyAnimator.PlayStopMove();
            }
        }
    }
}
