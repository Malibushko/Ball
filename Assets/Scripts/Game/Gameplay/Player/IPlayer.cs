using System;
using Core.Infrastructure.Interfaces;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public interface IPlayer : IActivatableService
    {
        public Action<Collision> OnHit { get; set; }
    }
}