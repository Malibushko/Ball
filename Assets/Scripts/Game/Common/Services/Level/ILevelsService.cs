using Core.Infrastructure.Interfaces;
using Game.Gameplay.Configs.Level;

namespace Game.Common.Services.Level
{
    public interface ILevelsService : IInitializableAsyncService
    {
        public LevelConfig GetCurrentLevelConfig();
        public bool AdvanceLevel();
        public void Reset();
    }
}