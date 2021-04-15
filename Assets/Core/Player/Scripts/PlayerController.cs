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
        private HealthControl _healthControl;
        private PlayerMovement _playerMovement;
   
        
        private Transform _childMesh;

        
            
        private void Awake()
        {
            _playerInput = new Player();
            _controller = GetComponent<CharacterController>();
            _playerMovement = GetComponent<PlayerMovement>();
            _healthControl = GameObject.Find(Conventions.HEALTH_HANDLER).GetComponent<HealthControl>();
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
            _childMesh = transform.GetChild(0).transform;
            _playerMovement.InitMovement(_controller, _playerInput, _childMesh);
            _healthControl.InitHealth();
        }

        private void Update()
        {
           _playerMovement.MovementControl();
        }
    }
}
   

