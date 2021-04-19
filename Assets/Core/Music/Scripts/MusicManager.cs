using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Core.Music.Scripts
{
    /// <summary>
    /// MusicManager is a singleton that handles the background music.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        private static MusicManager _instance;

        [SerializeField] private Sprite musicOnImage;
        [SerializeField] private Sprite musicOffImage;
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private GameObject musicButton;

        private AudioSource _audioSource;
        private Image _image;

        private void Awake()
        {
            if (_instance != null & _instance != this)
                Destroy(this.gameObject);
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);
                _audioSource = gameObject.GetComponent<AudioSource>();
                _audioSource.clip = backgroundMusic;
                _image = musicButton.GetComponent<Image>();
            }
        }

        private void Start()
        {
            _audioSource.Play();
        }

        /// <summary>
        /// Switchs the sprites of the music button.
        /// </summary>
        public void SwitchMusic()
        {
            if (_image.sprite == musicOnImage)
            {
                _image.sprite = musicOffImage;
                _audioSource.Pause();
            }
            else
            {
                _image.sprite = musicOnImage;
                _audioSource.Play();
            }
        }


    }
}
