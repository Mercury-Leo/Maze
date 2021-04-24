using System;

namespace Core.Player
{
    public class StateController : Singleton<StateController>
    {
        public static PlayerStates PlayerState = PlayerStates.Idle;
    
        public enum PlayerStates
        {
            Teleporting, 
            Walking, 
            Idle, 
            Jumping, 
            Spraying, 
            Winning,
            Losing
        }

        public void SetState(string state)
        {
            PlayerState = (PlayerStates)Enum.Parse(typeof(PlayerStates), state);
        }
        
        /// <summary>
        /// Checks if the player state is as needed.
        /// </summary>
        /// <param name="isState"></param>
        /// <returns></returns>
        public static bool IsState(PlayerStates isState)
        {
            return PlayerState.Equals(isState);
        }
    }
}
