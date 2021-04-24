using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Player.Scripts
{
    public class HealthControl : MonoBehaviour
    {
    
        private int _currentHealth;
    
        [SerializeField] private Sprite canFullHealth;
        [SerializeField] private Sprite canEmptyHealth;

        [SerializeField] private GameObject content;
        [SerializeField] private GameObject healthPrefab;
        

        /// <summary>
        /// Exposed init function for the PlayerController to call.
        /// </summary>
        public void InitHealth()
        {
            InitHealthPrefabs();
            _currentHealth = Conventions.PLAYER_MAX_HEALTH;
        }

        /// <summary>
        /// Adds health to the player, depends on amount given.
        /// </summary>
        /// <param name="amount"></param>
        public void AddHealth(int amount)
        {
            if (_currentHealth >= Conventions.PLAYER_MAX_HEALTH) return;

            for (var i = 0; i < amount; i++)
            {
                if (_currentHealth >= Conventions.PLAYER_MAX_HEALTH) return;
                _currentHealth++;
                content.transform.GetChild(_currentHealth - 1).transform.GetComponent<Image>().sprite = canFullHealth;
            }
        }

        /// <summary>
        /// Removes health to the player, depends on amount given. 
        /// </summary>
        /// <param name="amount"></param>
        public void RemoveHealth(int amount)
        {
            if (_currentHealth <= 0) return;

            for (var i = 0; i < amount; i++)
            {
                if (_currentHealth < 1) return;
                content.transform.GetChild(_currentHealth - 1).transform.GetComponent<Image>().sprite = canEmptyHealth;
                _currentHealth--;
            }
        }

        /// <summary>
        /// Creates prefabs of the health icon by the number of <see cref="Conventions.PLAYER_MAX_HEALTH"/>
        /// </summary>
        private void InitHealthPrefabs()
        {
            for (var i = 0; i < Conventions.PLAYER_MAX_HEALTH; i++)
            {
                Instantiate(healthPrefab, content.transform, true);
            }
        }
    }
}
