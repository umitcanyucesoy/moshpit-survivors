using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI_Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [Header("----- SCENE INDEX -----")]
        [SerializeField] private string firstSceneIndex;

        public void StartGame()
        {
            SceneManager.LoadScene(firstSceneIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}