using UniRx;

namespace Game.Gameplay.Services.Level
{
    public class LevelDataService : ILevelDataService
    {
        public ReactiveProperty<int> PlayerHealth { get; set; } = new();
        public ReactiveDictionary<Resource.Resource, int> Resources { get; set; } = new();
    }
}