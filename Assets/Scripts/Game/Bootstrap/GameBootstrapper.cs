using Cysharp.Threading.Tasks;
using Game.Common.Factories;
using Game.GameStates;
using UnityEngine;
using Zenject;

namespace Game.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private StateFactory _stateFactory;
        
        [Inject]
        public void Construct(GameStateMachine stateMachine, StateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }

        public void Start()
        {
            _stateMachine.RegisterState(_stateFactory.Create<GameBootstrapState>());
            _stateMachine.RegisterState(_stateFactory.Create<GameLoadingState>());
            _stateMachine.RegisterState(_stateFactory.Create<GameHubState>());
            _stateMachine.RegisterState(_stateFactory.Create<GamePlayState>());
            
            _stateMachine.GotoState<GameBootstrapState>().Forget();
        }
    }
}