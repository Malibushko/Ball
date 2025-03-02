using Cysharp.Threading.Tasks;
using Game.Common;
using Game.Common.Services.Loading;
using Game.Common.Services.Scenes;
using Game.Common.StateMachine;

namespace Game.GameStates
{
    public class GamePlayState : IState
    {
        private ILoadingScreen _loadingScreen;
        private ISceneLoader _sceneLoader;
        
        public GamePlayState(
            ILoadingScreen loadingScreen,
            ISceneLoader sceneLoader)
        {
            _loadingScreen = loadingScreen;
            _sceneLoader = sceneLoader;
        }
        
        public async UniTask Enter()
        {
            await _loadingScreen.Show();
            await _sceneLoader.LoadSceneAsync(SceneID.GamePlay);
        }

        public UniTask Exit() => default;
    }
}