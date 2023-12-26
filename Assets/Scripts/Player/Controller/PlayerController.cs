using System;
using Mirror;
using Player.Game;
using Player.Interfaces;
using Scripts.Dates;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using Scripts.Views;
using UnityEngine;
using VContainer;

namespace Scripts.Game
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    
    public class PlayerController : NetworkBehaviour, IPlayerController
    {
        [SerializeField] private PlayerCustomSettings _settings;

        [SyncVar] private string _playerName;
        
        private IPlayerAction[] _playerActions;
        private IBankService _bankService;
        private IGameView _gameView;
        private IPlayerView _playerView;

        private PlayerMovement _playerMovement;
        private PlayerGun _playerGun;

        private PlayerSkin _playerSkin;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;

        private Transform _transform;
        private Camera _camera;

        public Transform Transform => _transform;
        public Rigidbody2D Rigidbody => _rigidbody;
        public Animator Animator => _animator;
        public Collider2D Collider => _collider;
        public PlayerSkin PlayerSkin => _playerSkin;
        public bool IsLocalPlayer => isLocalPlayer;

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

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _playerSkin = GetComponentInChildren<PlayerSkin>();
            _animator = _playerSkin?.GetComponent<Animator>();
            
            _transform = transform;
            _camera = Camera.main;
            
            _playerView = Instantiate(_settings.PlayerViewPrefab, _transform.position, Quaternion.identity);

        }

        private void Start()
        {
            if (isClient && !isLocalPlayer)
                _playerView.SetName(_playerName);

            _playerActions = GetComponentsInChildren<IPlayerAction>();
            if (_playerActions is not { Length: > 0 }) 
                return;
            
            foreach (var action in _playerActions)
            {
                action.Initialize(this);
            }
            
            _playerGun = GetActionByType(PlayerActionType.Gun) as PlayerGun;
            _playerMovement = GetActionByType(PlayerActionType.Move) as PlayerMovement;
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
               _playerGun.Shoot();
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
            _playerSkin.SetVisible(true);
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

        private PlayerNetworkAction GetActionByType(PlayerActionType type)
        {
            foreach (var playerAction in _playerActions)
            {
                if (playerAction.Type == type)
                {
                   return playerAction as PlayerNetworkAction;
                }
            }

            return null;
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
