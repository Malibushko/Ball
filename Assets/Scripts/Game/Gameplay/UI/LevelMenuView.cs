using Cysharp.Threading.Tasks;
using Game.GameStates;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.Gameplay.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private string _actionButtonName = "ActionButton";
        [SerializeField] private string _quitLevelButtonName = "QuitButton";
        
        protected UIDocument _uiDocument;
        private Button _actionButton;
        private Button _quitButton;
        
        protected GameStateMachine _gameStateMachine;
        
        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            
            var root = _uiDocument.rootVisualElement;
            _actionButton = root.Q<Button>(_actionButtonName);
            _quitButton = root.Q<Button>(_quitLevelButtonName);
            
            _actionButton.clicked += ActionButtonClicked;
            _quitButton.clicked += QuitButtonOnClicked;
        }

        protected virtual void QuitButtonOnClicked()
        {
            _gameStateMachine.GotoState<GameHubState>().Forget();
        }

        protected virtual void ActionButtonClicked()
        {
            _gameStateMachine.GotoState<GamePlayState>().Forget();
        }
    }
}