using UnityEngine;

namespace Paranapiacaba.UI {
    public class SceneTransition : MonoSingleton<SceneTransition> {

        private MenuGroup _menuGroup;
        private Menu _transition;

        private void Start() {
            _menuGroup = GetComponent<MenuGroup>();
            _transition = GetComponent<Menu>();
            _menuGroup.CloseCurrentThenOpen(_transition);
        }

        public void Transition() {
            _menuGroup.CloseCurrentThenOpen(_transition);
        }

    }
}