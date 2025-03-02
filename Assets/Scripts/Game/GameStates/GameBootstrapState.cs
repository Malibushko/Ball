using Cysharp.Threading.Tasks;
using Game.Common.Services.Ads;
using Game.Common.Services.Analytics;
using Game.Common.Services.Assets;
using Game.Common.Services.Level;
using Game.Common.Services.Loading;
using Game.Common.Services.Text;
using Game.Common.Services.UI;
using Game.Common.StateMachine;

namespace Game.GameStates
{
    public class GameBootstrapState : IState
    {
        private GameStateMachine _stateMachine;
        private IGameStringsService _stringsService;
        private IAssetsService _assetsService;
        private ILoadingScreen _loadingScreen;
        private ILevelsService _levelsService;
        private IAdsService _adsService;
        private IAnalyticsService _analyticsService;
        private UIFactory _uiFactory;
        
        public GameBootstrapState(GameStateMachine stateMachine, 
            IGameStringsService stringsService,
            IAssetsService assetsService,
            ILoadingScreen loadingScreen,
            ILevelsService levelsService,
            IAdsService adsService,
            IAnalyticsService analyticsService,
            UIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _stringsService = stringsService;
            _assetsService = assetsService;
            _loadingScreen = loadingScreen;
            _levelsService = levelsService;
            _adsService = adsService;
            _analyticsService = analyticsService;
            _uiFactory = uiFactory;
        }
        
        public async UniTask Enter()
        {
            await InitializeServices();
            
            _stateMachine.GotoState<GameLoadingState>().Forget();
        }

        private async UniTask InitializeServices()
        {
            await _stringsService.InitializeAsync();
            await _assetsService.InitializeAsync();
            await _uiFactory.InitializeAsync();
            await _adsService.InitializeAsync();
            await _analyticsService.InitializeAsync();
            await _loadingScreen.InitializeAsync();
            await _levelsService.InitializeAsync();
        }

        public UniTask Exit() => default;
    }
}