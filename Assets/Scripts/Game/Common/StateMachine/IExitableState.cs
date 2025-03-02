using Cysharp.Threading.Tasks;

namespace Game.Common.StateMachine
{
    public interface IExitableState
    {
        public UniTask Exit();
    }
}