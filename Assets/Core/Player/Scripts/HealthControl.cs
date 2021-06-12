using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Player.Scripts
{
    public class HealthControl : MonoBehaviour
    {
        [SerializeField] private Sprite canFullHealth;
        [SerializeField] private Sprite canEmptyHealth;

        [SerializeField] private GameObject content;
        [SerializeField] private GameObject healthPrefab;
        
        public int CurrentHealth { get; private set; }
        
        /// <summary>
        /// Exposed init function for the PlayerController to call.
        /// </summary>
        public void InitializeSpray()
        {
            InitSprayPrefabs();
            CurrentHealth = Conventions.PLAYER_MAX_HEALTH;
        }

        /// <summary>
        /// Adds spray to the player, depends on amount given.
        /// </summary>
        /// <param name="amount"></param>
        public void AddSpray(int amount)
        {
            if (CurrentHealth >= Conventions.PLAYER_MAX_HEALTH) return;

            for (var i = 0; i < amount; i++)
            {
                if (CurrentHealth >= Conventions.PLAYER_MAX_HEALTH) return;
                CurrentHealth++;
                content.transform.GetChild(CurrentHealth - 1).transform.GetComponent<Image>().sprite = canFullHealth;
            }
        }

        /// <summary>
        /// Removes spray to the player, depends on amount given. 
        /// </summary>
        /// <param name="amount"></param>
        public void RemoveSpray(int amount = 1)
        {
            if (CurrentHealth <= 0) return;

            for (var i = 0; i < amount; i++)
            {
                if (CurrentHealth < 1) return;
                content.transform.GetChild(CurrentHealth - 1).transform.GetComponent<Image>().sprite = canEmptyHealth;
                CurrentHealth--;
            }
        }

        /// <summary>
        /// Creates prefabs of the health icon by the number of <see cref="Conventions.PLAYER_MAX_HEALTH"/>
        /// </summary>
        private void InitSprayPrefabs()
        {
            for (var i = 0; i < Conventions.PLAYER_MAX_HEALTH; i++)
            {
                Instantiate(healthPrefab, content.transform, true);
            }
        }
    }
}
