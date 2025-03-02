using Cysharp.Threading.Tasks;
using Game.Common.Services.Assets;
using Game.Common.Services.Camera;
using Game.Common.Services.Scenes;
using Game.Common.Services.UI;
using Game.Common.StateMachine;
using Game.Configs;
using Game.GameConfigs;
using Game.GameHub.Spawn;
using UnityEngine;

namespace Game.GameHub.States
{
    public class GameHubLoadingState : IState
    {
        private GameHubStateMachine _gameHubStateMachine;
        private GameHubConfig _gameHubConfig;
        private IPrefabInstantiator _prefabInstantiator;
        private IAssetsService _assetsService;
        private ICameraService _cameraService;
        private UIFactory _uiFactory;
        
        public GameHubLoadingState(
            GameHubStateMachine gameHubStateMachine,
            GameHubConfig gameHubConfig,
            IPrefabInstantiator prefabInstantiator,
            ICameraService cameraService,
            IAssetsService assetsService,
            UIFactory uiFactory)
        {
            _gameHubStateMachine = gameHubStateMachine;
            _gameHubConfig = gameHubConfig;
            _prefabInstantiator = prefabInstantiator;
            _cameraService = cameraService;
            _assetsService = assetsService;
            _uiFactory = uiFactory;
        }
        
        public UniTask Exit() => default;

        public UniTask Enter()
        {
            var level = SpawnLevel();
            var player = SpawnPlayer(level);
            
            _prefabInstantiator.Instantiate(_uiFactory.CreateGameHubHUD());
            
            InitCamera(player);

            return _gameHubStateMachine.GotoState<GameHubActiveState>();
        }
        
        private void InitCamera(GameObject playerObject)
        {
            _cameraService.LoadFromConfig(_gameHubConfig.CameraConfig);
            _cameraService.Follow(playerObject.transform);
        }

        private GameObject SpawnLevel()
        {
            return _prefabInstantiator.Instantiate(_assetsService.Load<GameObject>(_gameHubConfig.LevelPrefabPath));
        }

        private GameObject SpawnPlayer(GameObject level)
        {            
            var playerConfig = _gameHubConfig.PlayerConfig;
            var player = _prefabInstantiator.Instantiate(_assetsService.Load<GameObject>(playerConfig.PlayerPrefabPath));
            
            player.transform.parent = level.transform;
            
            var spawnPoint = level.GetComponentInChildren<PlayerSpawnPoint>();
            
            if (spawnPoint)
                player.transform.position = spawnPoint.transform.position;
            
            return player;
        }
    }
}