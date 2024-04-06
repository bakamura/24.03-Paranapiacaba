using Paranapiacaba.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Paranapiacaba.UI {
    public class Pause : MonoSingleton<Pause> {

        [Header("Events")]

        public UnityEvent<bool> onPause = new UnityEvent<bool>();

        [Header("Inputs")]

        [SerializeField] private InputActionReference _pauseInput;
        [SerializeField] private InputActionReference _unpauseInput;

        [Header("References")]

        [SerializeField] private MenuGroup _canvasMenuGroup;
        [SerializeField] private Menu _hud;
        [SerializeField] private Menu _pause;

        private void Start() {
            _pauseInput.action.started += (callBackContext) => PauseGame(true);
            _unpauseInput.action.started += (callBackContext) => PauseGame(false);
            onPause.AddListener(OpenMenu);
        }

        public void PauseGame(bool isPausing) {
            onPause?.Invoke(isPausing);
        }

        public void OpenMenu(bool isPausing) {
            Debug.Log($"OpenMenu {isPausing}");
            _canvasMenuGroup.CloseCurrentThenOpen(isPausing ? _pause : _hud);
            PlayerActions.Instance.ChangeInputMap((byte)(isPausing ? 1 : 0));
        }

    }
}