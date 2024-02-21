using Assets.Prefabs.Characters.MainHero.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenuController : Menu<LoseMenuController>
{
    [SerializeField] private GameObject _menuWindow;

    protected void Start()
    {
        HeroHealth.instance.OnDie.AddListener(ShowMenu); 
    }

    private void ShowMenu()
    {
        GameSession.instance.UiIsActive = true;
        _menuWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        _animator.SetTrigger(IsClose);
        onClose = () =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameSession.instance.UiIsActive = false;
        };
    }
    
    public void BackMainMenu()
    {
        GameSession.instance.UiIsActive = false;
        _animator.SetTrigger(IsClose);
        onClose = () =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        };
    }
}
