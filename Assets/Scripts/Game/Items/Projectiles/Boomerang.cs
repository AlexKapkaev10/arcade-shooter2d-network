using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Boomerang : Projectile
    {
        [SerializeField] private Rigidbody2D _rg;
        [SerializeField] private float _torqueSpeed = 50;

        public override void Shoot(Vector2 direction)
        {
            _rg.AddForce(direction * _shootSpeed, ForceMode2D.Impulse);
            _rg.AddTorque(_torqueSpeed, ForceMode2D.Impulse);
        }

        protected override void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage();
                Destroy(gameObject);
            }
        }
    }
}