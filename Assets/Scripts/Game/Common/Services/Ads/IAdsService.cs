using System;
using Core.Infrastructure.Interfaces;

namespace Game.Common.Services.Ads
{    
    public interface IAdsService : IInitializableAsyncService
    {
        Action<string> OnAdShowed { get; set; }
        
        bool CanShowAd();
        void LoadAd();
        bool ShowAd();
    }
}