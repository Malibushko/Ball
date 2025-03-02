using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.Loading
{
    [RequireComponent(typeof(UIDocument))]
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private string _progressBarControlName = "ProgressBar";
        [SerializeField] private string _loadingStatusLabelControlName = "LoadingStatusText";
     
        [SerializeField] private float _startProgress;
        [SerializeField] private float _endProgress = 1;

        private UIDocument _uiDocument;
        private ProgressBar _progressBar;
        private Label _loadingStatusLabel;
        
        private ILoadingProgressProvider _progressProvider;
        private List<IDisposable> _disposables = new();
        
        [Inject]
        public void Construct(ILoadingProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
        }
        
        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            var root = _uiDocument.rootVisualElement;
            
            _progressBar = root.Q<ProgressBar>(_progressBarControlName);
            _loadingStatusLabel = root.Q<Label>(_loadingStatusLabelControlName);

            _progressBar.lowValue = _startProgress;
            _progressBar.highValue = _endProgress;
            
            if (_progressProvider != null)
            {
                _progressProvider.NormalizedProgress.Subscribe(UpdateProgress).AddTo(_disposables);
                _progressProvider.DisplayStatus.Subscribe(UpdateStatus).AddTo(_disposables);
            }
        }
        
        private void OnDisable()
        {
            _disposables.ForEach(x => x.Dispose());
            _disposables.Clear();
        }

        private void UpdateProgress(float progress)
        {
            _progressBar.value = progress;
        }

        private void UpdateStatus(string status)
        {
            _loadingStatusLabel.text = status;
        }
    }
}