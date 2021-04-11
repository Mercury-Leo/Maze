namespace Core.Camera.Scripts
{
    using Cinemachine;
    using UnityEngine;
    using View.Input.Scripts;
    
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private float lookSpeed = 1;
        
        private CinemachineFreeLook _cinemachine;
        private Player _playerInput;

        private void Awake()
        {
            _playerInput = new View.Input.Scripts.Player();
            _cinemachine = GetComponent<CinemachineFreeLook>();
        }
        
        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        void Update()
        { 
            var delta = _playerInput.PlayerMain.LookAround.ReadValue<Vector2>();
            _cinemachine.m_XAxis.Value += delta.x + lookSpeed * Time.deltaTime;
            _cinemachine.m_YAxis.Value += delta.y + lookSpeed * Time.deltaTime;
        }
    }
}

