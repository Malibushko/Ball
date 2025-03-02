using Cysharp.Threading.Tasks;
using Game.Gameplay.Configs.Level;
using UnityEngine;

namespace Game.Gameplay.Services.Level
{
    public interface ILevelBuilder
    {
        public UniTask<GameObject> BuildLevel(LevelConfig levelConfig);
    }
}