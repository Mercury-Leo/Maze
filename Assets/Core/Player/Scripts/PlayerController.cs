using UnityEngine;
using View.Input.Scripts;

namespace Core.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {

        private View.Input.Scripts.Player _playerInput;
        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;

        [SerializeField]private float playerSpeed = 2.0f;
        [SerializeField]private float jumpHeight = 1.0f;
        [SerializeField]private float gravityValue = -9.81f;
            
        private void Awake()
        {
            _playerInput = new View.Input.Scripts.Player();
            _controller = GetComponent<CharacterController>();
        }
        
        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Start()
        {
        
        }

        private void Update()
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            var movementInput = _playerInput.PlayerMain.Move.ReadValue<Vector2>();
            var move = new Vector3(movementInput.x, 0f, movementInput.y);
            _controller.Move(move * (Time.deltaTime * playerSpeed));

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player..
            if (_playerInput.PlayerMain.Jump.triggered && _groundedPlayer)
            {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            _playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
   

