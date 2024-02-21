using Assets.Prefabs.Characters.MainHero.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Shoot_Kill.UI.Prefabs.PauseMenu.Scripts
{
    public class PauseMenuController : Menu<PauseMenuController>
    {
        [SerializeField] private GameObject _pauseWindow;
        [SerializeField] private GameObject _settingsWindow;
        
        public bool isOpen { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (HeroHealth.instance.isDie) return;
                    
                if (!isOpen)
                {
                    _pauseWindow.SetActive(true);
                    Time.timeScale = 0f;
                    GameSession.instance.UiIsActive = isOpen = true;
                }
                else
                {
                    _animator.SetTrigger(IsClose);
                    onClose = () =>
                    {
                        Time.timeScale = 1f;
                        GameSession.instance.UiIsActive = isOpen = false;
                        _pauseWindow.SetActive(false);
                    };
                }
            }
        }

        public void Resume()
        {
            _animator.SetTrigger(IsClose);
            onClose = () =>
            {
                Time.timeScale = 1f;
                GameSession.instance.UiIsActive = isOpen = false;
                _pauseWindow.SetActive(false);
            };
        }

        public void OpenSettings()
        {
            _pauseWindow.SetActive(false);
            _settingsWindow.SetActive(true);
        }

        public void BackMainMenu()
        {
            _animator.SetTrigger(IsClose);
            onClose = (() =>
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            });
        }
        
    }
}