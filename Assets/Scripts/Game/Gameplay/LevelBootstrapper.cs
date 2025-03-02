using Cysharp.Threading.Tasks;
using Game.Common.Factories;
using Game.Gameplay.States;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Gameplay
{
    public class LevelBootstrapper : MonoBehaviour
    {
        private LevelStateMachine _stateMachine;
        private StateFactory _stateFactory;
        
        [Inject]
        public void Construct(LevelStateMachine stateMachine, StateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }
        
        public void Start()
        {
            _stateMachine.RegisterState(_stateFactory.Create<LevelLoadingState>());
            _stateMachine.RegisterState(_stateFactory.Create<LevelPlayState>());
            _stateMachine.RegisterState(_stateFactory.Create<LevelCompletedState>());
            _stateMachine.RegisterState(_stateFactory.Create<LevelWinState>());
            _stateMachine.RegisterState(_stateFactory.Create<LevelLoseState>());

            _stateMachine.GotoState<LevelLoadingState>().Forget();
        }
    }
}