using Core.Player.Abilities.Scripts;
using UnityEngine;
namespace Core.Player.Scripts
{
    
    using View.Input.Scripts;
    
    /// <summary>
    /// The PlayerController will be in charge of controlling the player.
    /// Moving the player will come from the <see cref="PlayerMovement"/> Script.
    /// Abilities functionality will be used from the <see cref="PlayerAbilities"/> Script. 
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("Add here the empty Game objects that represent points into which to teleport to, make sure first one is player location.")]
        [SerializeField] private GameObject[] teleportArray;

        [SerializeField] private Animator playerAnimator;
        
        #region Scripts

        private CharacterController _controller;
        private HealthControl _healthControl;
        private PlayerMovement _playerMovement;
        private Teleport _teleport;
        private DrawGraffiti _graffiti;
        private AnimationController _playerAnimations;

        #endregion
       
        private Player _playerInput;
        private Transform _childMesh;
        private Vector2 _playerMovementInput;


        private void Awake()
        {
            _playerInput = new Player();
            _teleport = new Teleport(transform, teleportArray);
            _playerAnimations = new AnimationController(playerAnimator);
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
            _playerMovementInput = _playerInput.PlayerMain.Move.ReadValue<Vector2>();
            
            if (_playerMovementInput != Vector2.zero && StateController.IsState(StateController.PlayerStates.Idle))
            {
                StateController.PlayerState = StateController.PlayerStates.Walking;
                _playerMovement.MovementControl(_playerMovementInput);
                _playerAnimations.ChangeAnimationState(_playerMovementInput);
            }
            else
            {
                StateController.PlayerState = StateController.PlayerStates.Idle;
            }

            if (_playerInput.PlayerMain.Teleport.triggered)
            {
                StateController.PlayerState = StateController.PlayerStates.Teleporting;
                _teleport.TriggerAbility();
            }

            if (_playerInput.PlayerMain.Spray.triggered)
            {
                StateController.PlayerState = StateController.PlayerStates.Spraying;
                //.TriggerAbility();
            }
                
        }

        
    }
}