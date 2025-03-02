using Cysharp.Threading.Tasks;
using Game.Common.Services.Camera;
using Game.Common.Services.Loading;
using Game.Common.Services.Scenes;
using Game.Common.Services.Text;
using Game.Common.Services.UI;
using Game.Common.StateMachine;
using Game.Gameplay.Configs.Level;
using Game.Gameplay.Player;
using Game.Gameplay.Services.Level;
using Game.GameStates;

namespace Game.Gameplay.States
{
    public class LevelLoadingState : IState
    {
        private const string BuildingLevelStatusKey = "BuildingLevelStatus";
        private const string ConfiguringLevelStatusKey = "ConfiguringLevelStatus";
        private const string CreatingGameHUDStatusKey = "CreatingGameHUDStatus";
        private const string LoadingDoneStatusKey = "LoadingDoneStatus";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly LevelStateMachine _levelStateMachine;
        private readonly LevelConfig _levelConfig;
        private readonly ILoadingScreen _loadingScreen;
        private readonly ILevelBuilder _levelBuilder;
        private readonly ILevelService _levelService;
        private readonly IPrefabInstantiator _prefabInstantiator;
        private readonly UIFactory _uiFactory;
        private readonly ICameraService _cameraService;
        private readonly PlayerView _playerView;
        private readonly IGameStringsService _gameStringsService;
        
        public LevelLoadingState(
            GameStateMachine gameStateMachine,
            LevelStateMachine stateMachine,
            LevelConfig levelConfig,
            ILoadingScreen loadingScreen,
            ILevelBuilder levelBuilder,
            ILevelService levelService,
            IPrefabInstantiator prefabInstantiator,
            ICameraService cameraService,
            UIFactory uiFactory,
            PlayerView playerView, 
            IGameStringsService gameStringsService)
        {
            _gameStateMachine = gameStateMachine;
            _levelStateMachine = stateMachine;
            _levelConfig = levelConfig;
            _loadingScreen = loadingScreen;
            _levelBuilder = levelBuilder;
            _levelService = levelService;
            _prefabInstantiator = prefabInstantiator;
            _cameraService = cameraService;
            _uiFactory = uiFactory;
            _playerView = playerView;
            _gameStringsService = gameStringsService;
        }
        
        public async UniTask Enter()
        {
            await _loadingScreen.Show();
            _loadingScreen.SetDisplayStatus(_gameStringsService.GetString(BuildingLevelStatusKey));
            
            var level = await _levelBuilder.BuildLevel(_levelConfig);
            if (level == null)
            {
                _gameStateMachine.GotoState<GameHubState>().Forget();
                return;
            }
            
            _loadingScreen.SetDisplayStatus(_gameStringsService.GetString(ConfiguringLevelStatusKey));
            _levelService.Configure(level);
            
            _prefabInstantiator.Instantiate(_uiFactory.CreateGamePlayHUD());
            _loadingScreen.SetDisplayStatus(_gameStringsService.GetString(CreatingGameHUDStatusKey));
            _levelStateMachine.GotoState<LevelPlayState>().Forget();
            
            _cameraService.LoadFromConfig(_levelConfig.CameraConfig);
            _cameraService.Follow(_playerView.transform);
            
            _loadingScreen.SetDisplayStatus(_gameStringsService.GetString(LoadingDoneStatusKey));
        }
        
        public UniTask Exit() => default;
    }
}