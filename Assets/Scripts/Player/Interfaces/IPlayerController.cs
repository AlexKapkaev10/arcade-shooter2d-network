using Scripts.Game;
using UnityEngine;

namespace Player.Interfaces
{
    public interface IPlayerController
    {
        public Transform Transform { get; }
        public Rigidbody2D Rigidbody { get; }
        public Animator Animator { get; }
        public Collider2D Collider { get; }
        public PlayerSkin PlayerSkin { get; }
        public bool IsLocalPlayer { get; }
    }
}