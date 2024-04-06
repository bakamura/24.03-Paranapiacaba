using UnityEngine;
using Paranapiacaba.Player;
using Paranapiacaba.Scene;
using System.Collections;

namespace Paranapiacaba.UI {
    public class SaveSelector : MonoBehaviour {

        [SerializeField] private SceneLoader _mainMenu;
        [SerializeField] private SceneLoader _sceneToLoad;

        private void Awake() {
            PlayerActions.Instance.ChangeInputMap(1);
        }

        public void DisplaySaveInfo() {
            Debug.LogWarning("Method Not Implemented Yet");
        }

        public void EnterSave(int saveId) {
            PlayerActions.Instance.ChangeInputMap(0);
            SceneChangeFade.DoFade();
            StartCoroutine(EnterSaveRoutine());
        }

        private IEnumerator EnterSaveRoutine() {
            yield return new WaitForSeconds(SceneChangeFade.Instance.gameObject.GetComponent<Fade>().TransitionDuration);

            _mainMenu.UnloadScene();
            _sceneToLoad.LoadScene();
        }

    }
}