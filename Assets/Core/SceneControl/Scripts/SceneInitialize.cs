using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SceneControl.Scripts
{
    public class SceneInitialize : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(Conventions.SCENE_MAZE, LoadSceneMode.Single);
        }

    }
}
