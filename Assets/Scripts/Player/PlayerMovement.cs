using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rg;
        private Transform _transform;
        private Vector2 _input;

        private float _runSpeed;

        private bool _isLocalPlayer;
        private bool _isFlip;
        private bool _isFirstFlip;
        private bool _isRun;

        private readonly int Run = Animator.StringToHash("Run");

        public void Initialize(bool isLocalPlayer, float runSpeed)
        {
            _rg = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
            
            _isLocalPlayer = isLocalPlayer;
            _runSpeed = runSpeed;
            _transform = transform;
        }

        private void Update()
        {
            if (!_isLocalPlayer)
                return;

            ProcessInput();
            CalculateLookSide();
            SetRun(_input != Vector2.zero);
        }

        private void FixedUpdate()
        {
            if (!_isLocalPlayer)
                return;
            
            Move();
        }

        private void ProcessInput()
        {
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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

        private void Move()
        {
            if (_input == Vector2.zero || !_rg)
                return;
            
            _rg.MovePosition(_rg.position + _input * (_runSpeed * Time.deltaTime));
        }
    }
}