using Player.Dates;
using UnityEngine;

namespace Player.Interfaces
{
    public interface IPlayerGun
    {
        public Collider2D Collider { get; }
        public bool IsLocalPlayer { get; }

        public void HitDetect(in HitData hitData);
    }
}