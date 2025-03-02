using Cysharp.Threading.Tasks;

namespace Game.Common.StateMachine
{
    public interface IState : IExitableState
    {
        public UniTask Enter();
    }
}