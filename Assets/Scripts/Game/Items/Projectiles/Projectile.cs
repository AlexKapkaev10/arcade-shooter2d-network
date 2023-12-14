using UnityEngine;

namespace Scripts.Game
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected float _shootSpeed = 10;

        public virtual void Initialize() {}
        public virtual void Shoot(Vector2 direction){}

        protected virtual void OnCollisionEnter2D(Collision2D col) { }
    }
}