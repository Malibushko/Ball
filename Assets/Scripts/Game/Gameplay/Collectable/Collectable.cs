using Game.Gameplay.Services.Level;

namespace Game.Gameplay.Collectable
{
    public class Collectable
    {
        private Resource.Resource _resource;
        private int _amount;
        
        private ILevelProgressWatcherService _progressWatcher;
        
        public Collectable(ILevelProgressWatcherService progressWatcher)
        {
            _progressWatcher = progressWatcher;
        }

        public void Configure(Resource.Resource resource, int amount)
        {
            _resource = resource;
            _amount = amount;
        }

        public void Collect()
        {
            _progressWatcher.OnResourceCollected(_resource, _amount);
        }
    }
}