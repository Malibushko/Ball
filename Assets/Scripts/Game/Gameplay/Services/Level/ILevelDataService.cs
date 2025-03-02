using UniRx;

namespace Game.Gameplay.Services.Level
{
    public interface ILevelDataService
    {
        public ReactiveProperty<int> PlayerHealth { get; set; }
        public ReactiveDictionary<Resource.Resource, int> Resources { get; set; }
    }
}