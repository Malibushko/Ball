using Cysharp.Threading.Tasks;
using Game.GameStates;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.GameHub.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class GameHubHUDView : MonoBehaviour
    {
        [SerializeField] private string _playButtonName = "PlayButton";
        [SerializeField] private string _quitButtonName = "QuitButton";
        
        private GameStateMachine _stateMachine;
        private UIDocument _uiDocument;
        private Button _playButton;
        private Button _quitButton;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }
        
        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
        }

        private void Start()
        {
            var root = _uiDocument.rootVisualElement;
            
            _playButton = root.Q<Button>(_playButtonName);
            _quitButton = root.Q<Button>(_quitButtonName);
            
            _playButton.clicked += OnPlayButtonClicked;
            _quitButton.clicked += OnQuitButtonClicked;
        }
        
        private void OnDisable()
        {
            if (_playButton != null)
                _playButton.clicked -= OnPlayButtonClicked;
            
            if (_quitButton != null)
                _quitButton.clicked -= OnQuitButtonClicked;
        }
        
        private void OnPlayButtonClicked()
        {
            _stateMachine.GotoState<GamePlayState>().Forget();
        }
        
        private void OnQuitButtonClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else 
            Application.Quit();
#endif
        }
    }
}