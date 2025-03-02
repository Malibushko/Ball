using UniRx;

namespace Game.Gameplay.UI
{
    public interface ILevelDataViewModel
    {
        public IReadOnlyReactiveProperty<int> PlayerHealth { get; }
    }
}