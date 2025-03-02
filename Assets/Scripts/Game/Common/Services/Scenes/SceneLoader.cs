using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Common.Services.Scenes
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(SceneID sceneID, bool force = true, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var sceneName = GetSceneName(sceneID);
            var currentScene = SceneManager.GetActiveScene();
            if (force || currentScene.name != sceneName)
                await SceneManager.LoadSceneAsync(sceneName, mode);
        }

        public void LoadScene(SceneID sceneID, bool force = true, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var sceneName = GetSceneName(sceneID);
            var currentScene = SceneManager.GetActiveScene();
            if (force || currentScene.name != sceneName)
            {
                SceneManager.LoadScene(sceneName, mode);
            }
        }

        private string GetSceneName(SceneID sceneID)
        {
            var fieldInfo = sceneID.GetType().GetField(sceneID.ToString());
            var attributes = (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(System.ComponentModel.DescriptionAttribute), false);
    
            return attributes.Length > 0 ? attributes[0].Description : sceneID.ToString();
        }
    }
}