using Mirror;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Scripts.Game
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Collider2D))]
    
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private PlayerCustomSettings _settings;

        [SyncVar] private float _shootForce;

        private IBankService _bankService;

        private Body _body;
        private Collider2D _collider;
        private Transform _transform;
        private Camera _camera;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            if (!isLocalPlayer)
                return;
            
            _bankService = resolver.Resolve<IBankService>();
            resolver.Resolve<IGameView>().Initialize(_bankService);
            _bankService.Coins = 0;
        }

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
            _body = GetComponentInChildren<Body>();
            
            _transform = transform;
            _camera = Camera.main;

            _body?.Initialize(isLocalPlayer ? Color.white : _settings.ColorProxyBody);

            if (isServer)
                _shootForce = _settings.ShootForce;
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
            var projectile = Instantiate(
                _settings.GetProjectileByType(ProjectileType.Boomerang), 
                _transform.position,
                Quaternion.identity);
            
            NetworkServer.Spawn(projectile.gameObject);
            projectile.Initialize(_collider);
            projectile.Shoot(_body.transform.right, _shootForce);

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
                _bankService.Coins++;
            }
        }
    }
}
