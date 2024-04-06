using UnityEngine;
using Paranapiacaba.Player;

namespace Paranapiacaba.UI {
    public class SaveSelector : MonoBehaviour {

        private void Awake() {
            PlayerActions.Instance.ChangeInputMap(1);
        }

        public void DisplaySaveInfo() {
            Debug.LogWarning("Method Not Implemented Yet");
        }

        public void EnterSave(int saveId) {
            PlayerActions.Instance.ChangeInputMap(0);
        }

    }
}