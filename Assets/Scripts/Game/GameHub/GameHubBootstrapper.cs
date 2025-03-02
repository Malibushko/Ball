using Cysharp.Threading.Tasks;
using Game.Common.Factories;
using Game.GameHub.States;
using UnityEngine;
using Zenject;

namespace Game.GameHub
{
    public class GameHubBootstrapper : MonoBehaviour
    {
        private GameHubStateMachine _gameHubStateMachine;
        private StateFactory _stateFactory;

        [Inject]
        public void Construct(GameHubStateMachine gameHubStateMachine, StateFactory stateFactory)
        {
            _gameHubStateMachine = gameHubStateMachine;
            _stateFactory = stateFactory;
        }
        
        private void Start()
        {
            _gameHubStateMachine.RegisterState(_stateFactory.Create<GameHubLoadingState>());
            _gameHubStateMachine.RegisterState(_stateFactory.Create<GameHubActiveState>());

            _gameHubStateMachine.GotoState<GameHubLoadingState>().Forget();
        }
    }
}