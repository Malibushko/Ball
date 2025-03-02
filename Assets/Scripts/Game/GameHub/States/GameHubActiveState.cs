using Cysharp.Threading.Tasks;
using Game.Common.Services.Loading;
using Game.Common.StateMachine;

namespace Game.GameHub.States
{
    public class GameHubActiveState : IState
    {
        private ILoadingScreen _loadingScreen;

        public GameHubActiveState(ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }
        
        public UniTask Exit() => default;

        public async UniTask Enter()
        {
            await _loadingScreen.Hide();
        }
    }
}