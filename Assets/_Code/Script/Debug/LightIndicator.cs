using UnityEngine;
using Ivayami.Player.Ability;

public class LightIndicator : MonoBehaviour {

    private void Start() {
        GetComponent<AbilityGiver>().GiveAbility();
        Lantern.OnIlluminate.AddListener((v3) => transform.position = v3);
        //Lantern.OnIlluminate.AddListener((v3) => Debug.Log(v3));
    }

}
