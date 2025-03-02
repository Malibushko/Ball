using Game.Common.StateMachine;
using Zenject;

namespace Game.Common.Factories
{
    public class StateFactory
    {    
        private readonly DiContainer _container;

        protected StateFactory(DiContainer container)
        {
            _container = container;
        }
        
        public TState Create<TState>() where TState : IState
        {
            return _container.Instantiate<TState>();
        }
    }
}