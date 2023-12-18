using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Controllers
{
    public class InputController : MonoBehaviour, IInputController
    {
        private Vector2 _moveInput;

        public Vector2 MoveInput => _moveInput;
        
        private void Update()
        {
            ProcessInput(); 
        }
        
        private void ProcessInput()
        {
            _moveInput.x = Input.GetAxis("Horizontal"); 
            _moveInput.y = Input.GetAxis("Vertical");
        }
    }
}