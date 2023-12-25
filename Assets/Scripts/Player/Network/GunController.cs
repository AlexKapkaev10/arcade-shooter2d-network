using Mirror;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Game
{
    public class GunController : NetworkBehaviour
    {
        [SerializeField] private PlayerGunSettings _settings;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Body _body;
        
        private Transform _transform;
        
        [SyncVar] private float _shootForce;
        
        private void Start()
        {
            _transform = transform;
            
            if (isServer)
            {
                _shootForce = _settings.ShootForce;
            }
        }

        public void Shoot()
        {
            CmdShoot();
        }

        [Command]
        private void CmdShoot()
        {
            var projectile = Instantiate(
                _settings.GetProjectileByType(ProjectileType.Boomerang), 
                _transform.position,
                Quaternion.identity);
            
            NetworkServer.Spawn(projectile.gameObject);
            projectile.Initialize(_collider);
            projectile.Shoot(_body.transform.right, _shootForce);

        }
    }
}