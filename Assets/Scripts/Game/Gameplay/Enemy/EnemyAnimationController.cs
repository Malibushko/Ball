using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private string _stateAnimatorId = "State";
        
        private Animator _animator;
        private EnemyState _enemy;
        
        private IDisposable _stateDisposable;
        
        [Inject]
        public void Construct(EnemyState enemy)
        {
            _enemy = enemy;
            _stateDisposable = _enemy.CurrentState.Subscribe(OnStateChanged);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDisable()
        {
            _stateDisposable.Dispose();
        }

        private void OnStateChanged(EnemyState.State state)
        {
            if (!_animator)
                return;
            
            _animator.SetTrigger(state.ToString());

            if (_enemy.CurrentState.Value == EnemyState.State.Dying)
            {
                UniTask.WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length)
                    .ContinueWith(OnDeathAnimationFinished).Forget();
            }
        }

        private void OnDeathAnimationFinished()
        {
            _enemy.CurrentState.Value = EnemyState.State.Dead;
        }
    }
}