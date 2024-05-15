using UnityEngine;
using Ivayami.Player;

namespace Ivayami.Scene
{
    public class PlayerLockInput : MonoBehaviour
    {
        public void LockInput()
        {
            PlayerMovement.Instance.ToggleMovement(false);
            PlayerActions.Instance.ChangeInputMap(null);
        }

        public void UnlockInput()
        {
            PlayerMovement.Instance.ToggleMovement(true);
            PlayerActions.Instance.ChangeInputMap("Player");
        }
    }
}