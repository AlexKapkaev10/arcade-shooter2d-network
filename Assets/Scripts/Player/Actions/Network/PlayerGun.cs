using Mirror;
using Player.Dates;
using Player.Game;
using Player.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Game
{
    public class PlayerGun : PlayerNetworkAction, IPlayerAction, IPlayerGun
    {
        [SerializeField] private PlayerGunSettings _settings;
        
        private Collider2D _collider;
        private PlayerSkin _playerSkin;
        private Transform _transform;

        [SyncVar] private float _shootForce;

        public PlayerActionType Type => PlayerActionType.Gun;
        public Collider2D Collider => _collider;
        public bool IsLocalPlayer => isLocalPlayer;

        public void HitDetect(in HitData hitData)
        {
            CmdSetDamage(hitData.PlayerHealth, hitData.DamageValue);
        }

        public void Initialize(in IPlayerController playerController)
        {
            _collider = playerController.Collider;
            _playerSkin = playerController.PlayerSkin;
        }

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
            if (isServer)
                RpcShoot();
        }

        [Command]
        private void CmdShoot()
        {
            var projectile = Instantiate(
                _settings.GetProjectileByType(ProjectileType.Boomerang), 
                _transform.position,
                Quaternion.identity);

            projectile.Initialize(this);
            projectile.Shoot(_playerSkin.transform.right, _shootForce);
            
            NetworkServer.Spawn(projectile.gameObject);
        }

        [ClientRpc]
        private void RpcShoot()
        {
            var projectile = Instantiate(
                _settings.GetProjectileByType(ProjectileType.Boomerang), 
                _transform.position,
                Quaternion.identity);

            projectile.Initialize(this);
            projectile.Shoot(_playerSkin.transform.right, _shootForce);
        }

        [Command]
        private void CmdSetDamage(PlayerHealth targetPlayerHealth, byte damageValue)
        {
            targetPlayerHealth.Damage(damageValue);
        }
    }
}