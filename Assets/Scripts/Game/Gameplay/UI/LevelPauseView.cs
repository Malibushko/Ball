using Game.Gameplay.Services.Level;
using Zenject;

namespace Game.Gameplay.UI
{
    public class LevelPauseView : LevelMenuView
    {
        private ILevelService _levelService;

        [Inject]
        public void Construct(ILevelService levelService)
        {
            _levelService = levelService;
        }

        private void Start()
        {
            _levelService.Pause(true);
        }

        private void OnDisable()
        {
            _levelService.Pause(false);
        }

        protected override void ActionButtonClicked()
        {
            Destroy(gameObject);
        }
    }
}