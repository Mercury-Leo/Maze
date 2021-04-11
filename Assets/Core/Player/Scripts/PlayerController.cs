namespace Core.Player.Scripts
{
    using UnityEngine;
    using View.Input.Scripts;
    
    /// <summary>
    /// The PlayerController will be in charge of controlling the player.
    /// Moving the player will come from the <see cref="PlayerMovement"/> Script.
    /// Abilities functionality will be used from the <see cref="PlayerAbilities"/> Script. 
    /// </summary>
    public class PlayerController : MonoBehaviour
    {

        private Player _playerInput;
        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private Transform _cameraMain;
        private Transform _childMesh;

        [SerializeField]private float playerSpeed = Conventions.PLAYER_SPEED;
        [SerializeField]private float jumpHeight = Conventions.JUMP_HEIGHT;
        [SerializeField]private float gravityValue = Conventions.GRAVITY_VALUE;
        [SerializeField]private float rotationSpeed = Conventions.CHILD_ROTATION_SPEED;
            
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
            _cameraMain = Camera.main?.transform;
            _childMesh = transform.GetChild(0).transform;
        }

        private void Update()
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            var movementInput = _playerInput.PlayerMain.Move.ReadValue<Vector2>();
            var move = (_cameraMain.forward * movementInput.y + _cameraMain.right * movementInput.x);
            move.y = 0;
            //var move = new Vector3(movementInput.x, 0f, movementInput.y);
            _controller.Move(move * (Time.deltaTime * playerSpeed));

            /*
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }*/

            // Changes the height position of the player..
            if (_playerInput.PlayerMain.Jump.triggered && _groundedPlayer)
            {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            _playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);

            if (movementInput != Vector2.zero)
            {
                var localEulerAngles = _childMesh.localEulerAngles;
                var rotation = Quaternion.Euler(new Vector3(localEulerAngles.x, _cameraMain.localEulerAngles.y, localEulerAngles.z));
                _childMesh.rotation = Quaternion.Lerp(_childMesh.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
   

