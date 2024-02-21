using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Prefabs.Characters.MainHero.Scripts
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private HeroMovement _movement;

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector3>();
            _movement.SetDirection(direction);
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                _movement.Dash();
        }
    }
}
