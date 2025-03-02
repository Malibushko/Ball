using Cysharp.Threading.Tasks;
using Game.Common.Services.Loading;
using Game.Common.StateMachine;
using Game.Gameplay.Services.Level;

namespace Game.Gameplay.States
{
    public class LevelPlayState : IState
    {
        private ILoadingScreen _loadingScreen;
        private ILevelService _levelService;
        
        public LevelPlayState(ILoadingScreen loadingScreen, ILevelService levelService)
        {
            _loadingScreen = loadingScreen;
            _levelService = levelService;
        }
        
        public UniTask Exit() => default;

        public async UniTask Enter()
        {
            _levelService.Start();
            await _loadingScreen.Hide();
        }
    }
}