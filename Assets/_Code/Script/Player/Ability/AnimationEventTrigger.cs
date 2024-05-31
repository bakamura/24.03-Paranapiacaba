using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour {

    private static string _animationToTrigger;

    public void SetAnimationName(string animationName) {
        _animationToTrigger = animationName;
    }

    public void TriggerAnimationOn(string target) {
        transform.Find(target).GetComponent<Animator>().SetTrigger(_animationToTrigger);
    }

}
