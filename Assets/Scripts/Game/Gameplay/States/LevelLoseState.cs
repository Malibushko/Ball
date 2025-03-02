using Cysharp.Threading.Tasks;
using Game.Common.Services.Scenes;
using Game.Common.Services.UI;
using Game.Common.StateMachine;
using Game.Gameplay.Services.Level;
using UnityEngine;

namespace Game.Gameplay.States
{
    public class LevelLoseState : IState
    {
        private UIFactory _uiFactory;
        private IPrefabInstantiator _prefabInstantiator;
        private ILevelService _levelService;
        
        private GameObject _loseMenu;
        
        public LevelLoseState(
            UIFactory uiFactory,
            IPrefabInstantiator prefabInstantiator,
            ILevelService levelService)
        {
            _uiFactory = uiFactory;
            _prefabInstantiator = prefabInstantiator;
            _levelService = levelService;
        }

        public UniTask Exit()
        {
            if (_loseMenu)
                Object.Destroy(_loseMenu);
            
            return UniTask.CompletedTask;
        }

        public UniTask Enter()
        {
            _loseMenu = _prefabInstantiator.Instantiate(_uiFactory.CreateLevelLoseMenu());
            return UniTask.CompletedTask;
        }
    }
}