using Paranapiacaba.UI;

public class SceneChangeFade : MonoSingleton<SceneChangeFade> {

    private void Start() {
        Instance?.GetComponent<MenuGroup>().CloseCurrentThenOpen(Instance.GetComponent<Fade>());
    }

    public static void DoFade() {
        Instance?.GetComponent<MenuGroup>().CloseCurrentThenOpen(Instance.GetComponent<Fade>());
    }

}
