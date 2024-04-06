using Paranapiacaba.UI;

public class SceneChangeFade : MonoSingleton<SceneChangeFade> {

    public static void DoFade() {
        Instance?.GetComponent<MenuGroup>().CloseCurrentThenOpen(Instance.GetComponent<Fade>());
    }

}
