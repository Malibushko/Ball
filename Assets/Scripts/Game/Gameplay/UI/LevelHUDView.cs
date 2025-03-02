using System;
using System.Collections.Generic;
using Game.Common.Services.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using IPrefabInstantiator = Game.Common.Services.Scenes.IPrefabInstantiator;

namespace Game.Gameplay.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class LevelHUDView : MonoBehaviour
    {
        [SerializeField] private string _playerHealthTextId = "PlayerHealthText";
        [SerializeField] private string _backButtonId = "BackButton";
        
        private UIDocument _uiDocument;
        private Label _playerHealthText;
        private Button _backButton;
        
        private ILevelDataViewModel _levelData;
        private UIFactory _uiFactory;
        private IPrefabInstantiator _prefabInstantiator;
        private List<IDisposable> _disposables = new();
        
        [Inject]
        public void Construct(ILevelDataViewModel levelDataViewModel, UIFactory uiFactory, IPrefabInstantiator prefabInstantiator)
        {
            _levelData = levelDataViewModel;
            _uiFactory = uiFactory;
            _prefabInstantiator = prefabInstantiator;
        }

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            var root = _uiDocument.rootVisualElement;
            
            _playerHealthText = root.Q<Label>(_playerHealthTextId);
            _backButton = root.Q<Button>(_backButtonId);
            _backButton.clicked += OnBackButtonClicked;
        }

        private void Start()
        {
            _levelData.PlayerHealth.Subscribe(_ => UpdateControls()).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.ForEach(x => x.Dispose());
            _disposables.Clear();
        }

        private void UpdateControls()
        {
            _playerHealthText.text = _levelData.PlayerHealth.ToString();
        }
        
        private void OnBackButtonClicked()
        {
            _prefabInstantiator.Instantiate(_uiFactory.CreateLevelPauseMenu());
        }
    }
}