using Mirror;
using Scripts.Controllers;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(InputController))]
    public sealed class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private float _runSpeedSetter = 3;
        [SerializeField] private Rigidbody2D _rg;
        [SerializeField] private Animator _animator;

        private InputController _inputController;
        private Transform _transform;
        private Vector2 _input;
        private bool _gameIsRun;
        private bool _isFlip;
        private bool _isFirstFlip;
        private bool _isRun;
        private readonly int Run = Animator.StringToHash("Run");

        [SyncVar] private float _runSpeed;

        public void SetGameRun(bool gameIsRun)
        {
            _gameIsRun = gameIsRun;
        }

        public void Start()
        {
            _inputController = GetComponent<InputController>();
            
            if (isServer)
                _runSpeed = _runSpeedSetter;
            
            _transform = _rg.transform;
        }

        private void Update()
        {
            if (!isLocalPlayer || !_gameIsRun)
                return;

            ProcessInput();
            CalculateLookSide();
            SetRun(_input != Vector2.zero);
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer|| !_gameIsRun)
                return;
            
            CmdMove();
            Move();
        }

        private void ProcessInput()
        {
            _input = _inputController.MoveInput;
        }

        private void SetRun(bool value)
        {
            if (_isRun == value || !_animator)
                return;

            _isRun = value;
            _animator.SetBool(Run, _isRun);
        }

        private void CalculateLookSide()
        {
            if (_input.x < 0 && !_isFirstFlip)
            {
                _isFirstFlip = true;
                SetLocalRotation(Quaternion.Euler(0, 180, 0));
            }
            else if (_input.x > 0 && !_isFirstFlip)
            {
                _isFirstFlip = true;
            }

            if (_input.x > 0 && !_isFlip)
            {
                _isFlip = true;
                SetLocalRotation(Quaternion.Euler(0, 0, 0));
            }
            else if (_input.x < 0 && _isFlip)
            {
                _isFlip = false;
                SetLocalRotation(Quaternion.Euler(0, 180, 0));
            }
            
            void SetLocalRotation(Quaternion value)
            {
                if (_transform.localRotation == value)
                    return;

                _transform.localRotation = value;
            }
        }

        [Command]
        private void CmdMove()
        {
            if (_input == Vector2.zero || !_rg)
                return;
            
            _rg.MovePosition(_rg.position + _input.normalized * (_runSpeed * Time.deltaTime));
        }
        
        private void Move()
        {
            if (_input == Vector2.zero || !_rg)
                return;
            
            _rg.MovePosition(_rg.position + _input.normalized * (_runSpeed * Time.deltaTime));
        }
    }
}