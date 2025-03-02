using Cysharp.Threading.Tasks;
using Game.Common.Services.Level;
using Game.Common.Services.Scenes;
using Game.Common.Services.UI;
using Game.Common.StateMachine;
using Game.Gameplay.Services.Level;

namespace Game.Gameplay.States
{
    public class LevelWinState : IState
    {
        private UIFactory _uiFactory;
        private IPrefabInstantiator _prefabInstantiator;
        private ILevelsService _levelsService;

        public LevelWinState(
            UIFactory uiFactory,
            IPrefabInstantiator prefabInstantiator,
            ILevelsService levelsServicee)
        {
            _uiFactory = uiFactory;
            _prefabInstantiator = prefabInstantiator;
            _levelsService = levelsServicee;
        }
        
        public UniTask Exit() => default;

        public UniTask Enter()
        {
            if (!_levelsService.AdvanceLevel())
                _levelsService.Reset();
            
            _prefabInstantiator.Instantiate(_uiFactory.CreateLevelWinMenu());
            
            return default;
        }
    }
}