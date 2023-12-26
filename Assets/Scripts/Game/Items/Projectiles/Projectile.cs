using Player.Interfaces;
using UnityEngine;

namespace Scripts.Game
{
    public class Projectile : MonoBehaviour
    {
        public virtual void Initialize(IPlayerGun owner) {}
        public virtual void Shoot(Vector2 direction, float shootForce){}
        protected virtual void OnCollisionEnter2D(Collision2D col) { }
        protected virtual void OnTriggerEnter2D(Collider2D col) { }
    }
}