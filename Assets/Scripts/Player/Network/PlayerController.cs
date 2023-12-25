using Mirror;
using Scripts.Dates;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using Scripts.Views;
using UnityEngine;
using VContainer;

namespace Scripts.Game
{
    [RequireComponent(typeof(Collider2D))]
    
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private PlayerCustomSettings _settings;
        
        [SyncVar] private string _playerName;

        private IBankService _bankService;
        private IGameView _gameView;
        private IPlayerView _playerView;

        private PlayerMovement _playerMovement;
        private GunController _gunController;
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
            _gameView = resolver.Resolve<IGameView>();
            _gameView.Initialize(_bankService);
            _gameView.OnSetPlayer += SetPlayerData;
            _bankService.Coins = 0;
        }

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
            _gunController = GetComponentInChildren<GunController>();
            _playerMovement = GetComponentInChildren<PlayerMovement>();
            _body = GetComponentInChildren<Body>();
            _playerView = Instantiate(_settings.PlayerViewPrefab, transform.position, Quaternion.identity);
            _transform = transform;
            _camera = Camera.main;

            _body?.Initialize(isLocalPlayer ? Color.white : _settings.ColorProxyBody);

            if (isClient && !isLocalPlayer)
            {
                _playerView.SetName(_playerName);
            }

            if (isLocalPlayer)
            {
                _body.SetVisible(false);
            }
        }

        private void Update()
        {
            if (string.IsNullOrEmpty(_playerName))
                return;
            
            _playerView.SetPosition(_transform.position);
            
            if (!isLocalPlayer)
                return;
            
            if (Input.GetMouseButtonDown(0))
            {
               _gunController.Shoot();
            }
        }

        private void LateUpdate()
        {
            if (!isLocalPlayer)
                return;
            
            CameraMovement();
        }

        private void OnDestroy()
        {
            if (_playerView != null)
                Destroy(_playerView.GameObject);
        }

        private void SetPlayerData(PlayerData data)
        {
            _gameView.OnSetPlayer -= SetPlayerData;
            _playerMovement.SetGameRun(true);
            _playerView.SetName(data.Name);
            CmdSetName(data.Name);
            _body.SetVisible(true);
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

        [Command]
        private void CmdSetName(string value)
        {
            _playerName = value;
            _playerView.SetName(_playerName);
            RpcSetName(value);
        }

        [ClientRpc]
        private void RpcSetName(string playerName)
        {
            _playerName = playerName;
            _playerView.SetName(_playerName);
        }
    }
}
