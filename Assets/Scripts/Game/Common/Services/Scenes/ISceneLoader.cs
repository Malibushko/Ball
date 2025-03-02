using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Common.Services.Scenes
{
    public interface ISceneLoader
    {
        public UniTask LoadSceneAsync(SceneID sceneID, bool force = true, LoadSceneMode mode = LoadSceneMode.Single);
        
        public void LoadScene(SceneID scene, bool force = true, LoadSceneMode mode = LoadSceneMode.Single);
    }
}