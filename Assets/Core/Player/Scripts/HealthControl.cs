using UnityEngine;
using UnityEngine.UI;

namespace Core.Player.Scripts
{
    public class HealthControl : MonoBehaviour
    {
    
        private int _currentHealth;
    
        [SerializeField] private Sprite _canFullHealth;
        [SerializeField] private Sprite _canEmptyHealth;

        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _healthPrefab;
        

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
                _content.transform.GetChild(_currentHealth - 1).transform.GetComponent<Image>().sprite = _canFullHealth;
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
                _content.transform.GetChild(_currentHealth - 1).transform.GetComponent<Image>().sprite = _canEmptyHealth;
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
                Instantiate(_healthPrefab, _content.transform, true);
            }
        }
    }
}
