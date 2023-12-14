using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Boomerang : Projectile
    {
        private Rigidbody2D _rg;

        private void Awake()
        {
            _rg = GetComponent<Rigidbody2D>();
        }

        public override void Shoot(Vector2 direction)
        {
            _rg.AddForce(direction);
        }
    }
}