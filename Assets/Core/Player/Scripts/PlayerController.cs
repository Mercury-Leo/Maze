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
        
        #region Scripts

        private CharacterController _controller;
        private HealthControl _healthControl;
        private PlayerMovement _playerMovement;
        private Teleport _teleport;

        #endregion
       
        private Player _playerInput;
        private Transform _childMesh;
        private PlayerStates _playerState = PlayerStates.Idle;

        private enum PlayerStates
        {
            Teleporting, 
            Walking, 
            Idle, 
            Jumping, 
            Spraying, 
            Winning,
            Losing
        }
        
            
        private void Awake()
        {
            _playerInput = new Player();
            _controller = GetComponent<CharacterController>();
            _playerMovement = GetComponent<PlayerMovement>();
            _healthControl = GameObject.Find(Conventions.HEALTH_HANDLER).GetComponent<HealthControl>();
            _teleport = new Teleport(transform, teleportArray);
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
            if(IsState(PlayerStates.Walking))
                _playerMovement.MovementControl();
            
            if(IsState(PlayerStates.Teleporting))
                _teleport.TriggerAbility();
        }

        /// <summary>
        /// Checks if the player is state is as needed.
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="isState"></param>
        /// <returns></returns>
        private bool IsState(PlayerStates isState)
        {
            return _playerState.Equals(isState);
        }
    }
}
   

