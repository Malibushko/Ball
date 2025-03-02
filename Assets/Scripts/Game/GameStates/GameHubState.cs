using Cysharp.Threading.Tasks;
using Game.Common;
using Game.Common.Services.Loading;
using Game.Common.Services.Scenes;
using Game.Common.Services.Text;
using Game.Common.StateMachine;

namespace Game.GameStates
{
    public class GameHubState : IState
    {
        private const string LoadingGameHubStatusKey = "LoadingGameHubStatus";
        
        private ILoadingScreen _loadingScreen;
        private ISceneLoader _sceneLoader;
        private IGameStringsService _gameStringsService;
        
        public GameHubState(
            ILoadingScreen loadingScreen, 
            ISceneLoader sceneLoader,
            IGameStringsService gameStringsService)
        {
            _loadingScreen = loadingScreen;
            _sceneLoader = sceneLoader;
            _gameStringsService = gameStringsService;
        }
        
        public async UniTask Enter()
        {
            await _loadingScreen.Show();
            
            _loadingScreen.SetDisplayStatus(_gameStringsService.GetString(LoadingGameHubStatusKey));
            await _sceneLoader.LoadSceneAsync(SceneID.Hub);
        }
        
        public UniTask Exit()
        {
            _loadingScreen.Hide();
            return default;
        }
    }
}