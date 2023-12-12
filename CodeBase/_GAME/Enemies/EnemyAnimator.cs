using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace CodeBase._GAME.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        public Animator Animator;

        private int _hitMax = 2;

        private int _speed = Animator.StringToHash("Speed");
        private int _hit = Animator.StringToHash("Hit");
        private int _isHit = Animator.StringToHash("IsHit");
        private int _attack = Animator.StringToHash("Attack");

        private int _hitLayer = 1;
        private float _currentSpeed = 0f;
        private bool _isMoving = false;
        private bool _isRunning = false;

        private TweenerCore<float, float, FloatOptions> _tweenSpeed;

        public void PlayStopMove()
        {
            if (!_isMoving || !_isRunning) return;

            _isMoving = false;
            _isRunning = false;

            TweenSpeed(0f, () => Animator.SetFloat(_speed, _currentSpeed));
        }

        public void PlayWalk()
        {
            if (_isMoving) return;

            _isMoving = true;

            TweenSpeed(0.5f, () => Animator.SetFloat(_speed, _currentSpeed));
        }

        public void PlayRun()
        {
            if (_isRunning) return;

            _isRunning = true;

            TweenSpeed(_currentSpeed, () => Animator.SetFloat(_speed, 1f));
        }

        public void PlayOnHit()
        {
            Animator.SetInteger(_hit, Random.Range(1, _hitMax + 1));
            Animator.SetTrigger(_isHit);
        }

        public void OffHitLayer()
        {
            Animator.SetLayerWeight(_hitLayer, 0f);
        }

        public void PlayOnAttack(bool attack) => Animator.SetBool(_attack, attack);

        private void TweenSpeed(float endValue, System.Action onUpdate = null)
        {
            if (_tweenSpeed != null && _tweenSpeed.active)
            {
                _tweenSpeed?.Kill();
                _tweenSpeed = null;
            }

            _tweenSpeed = DOTween.To(() => _currentSpeed, x => _currentSpeed = x, endValue, 0.25f).OnUpdate(() =>
            {
                onUpdate?.Invoke();
            });
        }
    }
}