using Game.Common;
using Game.Common.Services.Scenes;
using UnityEngine;
using Zenject;

namespace Game.Game
{
    public class GameRunner : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void Awake()
        {
            _sceneLoader.LoadScene(SceneID.Boostrap, force: false);
        }
    }
}