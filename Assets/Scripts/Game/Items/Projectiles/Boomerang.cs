using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Boomerang : Projectile
    {
        [SerializeField] private Rigidbody2D _rg;
        [SerializeField] private float _torqueSpeed = 50;
        [SerializeField] private Collider2D _collider;

        private bool _isInitialize = false;

        public override void Initialize(Collider2D ownerCollider)
        {
            Physics2D.IgnoreCollision(ownerCollider, _collider);
            _isInitialize = true;
        }

        public override void Shoot(Vector2 direction, float shootForce)
        {
            _rg.AddForce(direction.normalized * shootForce, ForceMode2D.Impulse);
            _rg.AddTorque(_torqueSpeed, ForceMode2D.Impulse);
            Destroy(gameObject, 10);
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (!_isInitialize || !col.gameObject.TryGetComponent(out IDamageable damageable)) 
                return;
            
            damageable.Damage();
            Destroy(gameObject);
        }
    }
}