using Mirror;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private PlayerCustomSettings _settings;
        [SerializeField] private Animator _animator;
        
        private Transform _transform;
        private Rigidbody2D _rg;
        private Camera _camera;
        private Vector2 _input;

        private readonly int Run = Animator.StringToHash("Run");
        private bool _isFlip;
        private bool _isFirstFlip;
        private bool _isRun;

        private void Start()
        {
            _transform = transform;
            _rg = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!isLocalPlayer)
                return;

            ProcessInput();
            CalculateLookSide();
            SetRun(_input != Vector2.zero);
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer)
                return;
            
            PlayerMovement();
        }

        private void LateUpdate()
        {
            if (!isLocalPlayer)
                return;
            
            CameraMovement();
        }

        private void ProcessInput()
        {
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        private void SetRun(bool value)
        {
            if (_isRun == value)
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

        private void PlayerMovement()
        {
            if (_input == Vector2.zero)
                return;
            
            _rg.MovePosition(_rg.position + _input * (_settings.RunSpeed * Time.deltaTime));
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
