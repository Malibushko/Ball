using UniRx;

namespace Core.Infrastructure.Interfaces
{
    public interface IProgressProvider
    {
        public IReadOnlyReactiveProperty<float> NormalizedProgress { get; }
        public IReadOnlyReactiveProperty<string> DisplayStatus { get; }
    }
}