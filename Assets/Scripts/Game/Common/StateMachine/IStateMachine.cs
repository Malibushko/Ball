using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Game.Common.StateMachine
{
    public class StateMachine
    {
        private List<IState> _states = new();
        private IState _current;

        public void RegisterState(IState state)
        {
            _states.Add(state);
        }

        public void UnregisterState(IState state)
        {
            _states.Remove(state);
        }
        
        public async UniTask<bool> GotoState<TIState>() where TIState : IState
        {
            if (_current != null)
                await _current.Exit();
            
            _current = _states.Find(x => x.GetType() == typeof(TIState));
            if (_current == null)
                return false;
            
            await _current.Enter();
            return true;
        }
    }
}