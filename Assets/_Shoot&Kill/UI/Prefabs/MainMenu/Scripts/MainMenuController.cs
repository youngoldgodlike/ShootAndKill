using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.UI.Prefabs.MainMenu.Scripts
{
    public class MainMenuController : Menu<MainMenuController>
    {
        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _settingsWindow;
        
        public void StartLevel()
        {
            onClose = () =>  SceneManager.LoadScene("Saves"); 
            _animator.SetTrigger(IsClose);
        }

        public void ExitGame()
        {
            onClose = () =>  Application.Quit();
            _animator.SetTrigger(IsClose);
        } 

        public void OpenSettings()
        {
            _menuWindow.SetActive(false);
            _settingsWindow.SetActive(true);
        }
        
    }
}
