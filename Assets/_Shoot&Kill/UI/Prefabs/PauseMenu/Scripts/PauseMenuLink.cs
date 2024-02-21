using UnityEngine;

namespace _Shoot_Kill.UI.Prefabs.PauseMenu.Scripts
{
    public class PauseMenuLink : MonoBehaviour
    {
        public void OnClose() => PauseMenuController.Instance.onClose?.Invoke();

    }
}