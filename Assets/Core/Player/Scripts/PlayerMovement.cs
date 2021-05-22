using System;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.Camera;

namespace Core.Player.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector3 _playerVelocity;
        private bool _groundedPlayer;
        private CharacterController _controller;
        private Transform _cameraMain;
        private View.Input.Scripts.Player _playerInput;
        private Transform _childMesh;
        
        [SerializeField]private float playerSpeed = Conventions.PLAYER_SPEED;
        [SerializeField]private float jumpHeight = Conventions.JUMP_HEIGHT;
        [SerializeField]private float gravityValue = Conventions.GRAVITY_VALUE;
        [SerializeField]private float rotationSpeed = Conventions.CHILD_ROTATION_SPEED;

        public void InitMovement(CharacterController characterController, View.Input.Scripts.Player player,
            Transform child)
        {
            _controller = characterController;
            _playerInput = player;
            _childMesh = child;
        }
        
        
        private void Start()
        {
            Assert.IsNotNull(main);
            _cameraMain = main?.transform;
        }

        public void MovementControl(Vector2 movementInput)
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            //var movementInput = _playerInput.PlayerMain.Move.ReadValue<Vector2>();
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
