using Mirror;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private PlayerCustomSettings _settings;
        [SerializeField] private Body _body;
        
        private PlayerMovement _playerMovement;
        private Transform _transform;
        private Camera _camera;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerMovement?.Initialize(isLocalPlayer, _settings.RunSpeed);
            _body?.Initialize(isLocalPlayer ? Color.white : _settings.ColorProxyBody);
            _transform = transform;
            _camera = Camera.main;

            gameObject.layer = isLocalPlayer ? LayerMask.NameToLayer("Client") : LayerMask.NameToLayer("Proxy");
        }

        private void Update()
        {
            if (!isLocalPlayer)
                return;
            
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void LateUpdate()
        {
            if (!isLocalPlayer)
                return;
            
            CameraMovement();
        }

        [Command]
        private void Shoot()
        {
            Projectile projectile = Instantiate(
                _settings.GetProjectileByType(ProjectileType.Boomerang), 
                _transform.position, 
                Quaternion.identity);
            NetworkServer.Spawn(projectile.gameObject);

            projectile.Shoot(_transform.right);
        }

        private void CameraMovement()
        {
            _camera.transform.localPosition = _transform.localPosition + new Vector3(0, 0, -5);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
    }
}
