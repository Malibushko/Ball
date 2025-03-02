namespace Game.Gameplay.Services.Level
{
    public interface ILevelProgressWatcherService
    {
        public void OnPlayerHealthChanged(int playerHealth);
        public void OnResourceCollected(Resource.Resource resource, int amount);
        public void OnWinZoneReached();
    }
}