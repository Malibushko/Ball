using Cysharp.Threading.Tasks;
using Game.Common.Services.Loading;
using Game.Common.Services.Text;
using Game.Common.Services.UI;
using Game.Common.StateMachine;

namespace Game.GameStates
{
    public class GameLoadingState : IState
    {
        private const string PreloadingUIStatusKey = "PreloadingUIStatus";
        private const string LoadingGameDoneStatusKey = "LoadingGameDoneStatus";
        
        private GameStateMachine _stateMachine;
        private ILoadingScreen _loadingScreen;
        private UIFactory _uiFactory;
        private IGameStringsService _stringsService;
        
        public GameLoadingState(
            GameStateMachine stateMachine, 
            ILoadingScreen loadingScreen,
            UIFactory uiFactory,
            IGameStringsService gameStringsService)
        {
            _stateMachine = stateMachine;
            _loadingScreen = loadingScreen;
            _uiFactory = uiFactory;
            _stringsService = gameStringsService;
        }
        
        public async UniTask Enter()
        {
            await _loadingScreen.Show();
            _loadingScreen.SetDisplayStatus(_stringsService.GetString(PreloadingUIStatusKey));
            
            await _uiFactory.PreloadUI();
            
            _loadingScreen.SetDisplayStatus(_stringsService.GetString(LoadingGameDoneStatusKey));
            _stateMachine.GotoState<GameHubState>().Forget();
        }
        
        public UniTask Exit() => default;
    }
}