using Player.Dates;
using Player.Interfaces;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Boomerang : Projectile
    {
        [SerializeField] private Rigidbody2D _rg;
        [SerializeField] private float _torqueSpeed = 50;
        [SerializeField] private Collider2D _collider;
        
        private bool _isInitialize = false;
        private IPlayerGun _owner;
        private byte _damageValue = 1;

        public override void Initialize(IPlayerGun owner)
        {
            Physics2D.IgnoreCollision(owner.Collider, _collider);
            _isInitialize = true;
            _owner = owner;
            
        }

        public override void Shoot(Vector2 direction, float shootForce)
        {
            _rg.AddForce(direction.normalized * shootForce, ForceMode2D.Impulse);
            _rg.AddTorque(_torqueSpeed, ForceMode2D.Impulse);
            Destroy(gameObject, 10);
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            var damageable = col.gameObject.GetComponentInChildren<IDamageable>();
            
            if (!_isInitialize || damageable == null) 
                return;
            
            Debug.Log(_owner.IsLocalPlayer);
            
            if (_owner.IsLocalPlayer)
            {
                HitData hitData = new ()
                {
                    PlayerHealth = damageable.PlayerHealth,
                    DamageValue = _damageValue
                };

                _owner.HitDetect(hitData);
            }

            Destroy(gameObject);
        }
    }
}