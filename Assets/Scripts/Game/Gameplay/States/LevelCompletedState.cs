using Cysharp.Threading.Tasks;
using Game.Common.Services.Camera;
using Game.Common.StateMachine;
using Game.Gameplay.Player;
using Game.Gameplay.Services.Level;
using Game.Gameplay.Zones;

namespace Game.Gameplay.States
{
    public class LevelCompletedState : IState
    {
        private ILevelService _levelService;
        private ICameraService _cameraService;
        private PlayerView _player;
        private LevelStateMachine _levelStateMachine;

        public LevelCompletedState(ILevelService levelService, ICameraService camera, PlayerView player, LevelStateMachine levelStateMachine)
        {
            _levelService = levelService;
            _cameraService = camera;
            _player = player;
            _levelStateMachine = levelStateMachine;
        }
        
        public async UniTask Enter()
        {
            var winZone = _levelService.FindComponentOnLevel<WinZone>(true);
            if (winZone)
            {
                _cameraService.Follow(winZone.transform);
                
                await winZone.Activate();
                
                _cameraService.Follow(_player.transform);
            }
            else
            {
                _levelStateMachine.GotoState<LevelWinState>().Forget();
            }
        }
        
        public UniTask Exit() => default;
    }
}