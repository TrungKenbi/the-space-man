using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private AudioSource sndButtonEnter;

        private void OnValidate()
        {
            sndButtonEnter = GetComponent<AudioSource>();
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }

        public void MouseHoverButton()
        {
            sndButtonEnter.Play();
        }

    }
}

