using UnityEngine;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class UIMainMenuController : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        private AudioSource _audioSource = new();

        public void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = audioClip;

            if (_audioSource.clip == null) return;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void Continue()
        {
            // TODO - Add New Game function
        }

        public void NewGame()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadGame()
        {
            // TODO - Add Load Game function
        }

        public void OpenSettings()
        {
            // TODO - Add open settings function
        }

        public void ExitGame()
        {
            Application.Quit();
        }

    }
}