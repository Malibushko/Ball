using UnityEngine;

namespace Game.Gameplay.Services.Level
{
    public enum LevelResult
    {
        Win,
        Lose
    }
    
    public interface ILevelService
    {
        public void Configure(GameObject level);

        public void Start();
        public void Finish(LevelResult result);
        public void Pause(bool pause);
        
        public TComponent FindComponentOnLevel<TComponent>(bool includeInactive = false) where TComponent : Object; 
    }
}