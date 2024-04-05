using UnityEngine;
using UnityEngine.Events;

namespace Paranapiacaba.Player {
    public class PlayerStress : MonoSingleton<PlayerStress> {

        [Header("Events")]

        public UnityEvent<float> onStressChange = new UnityEvent<float>();
        public UnityEvent onFailState = new UnityEvent();

        [Header("Parameters")]

        [SerializeField] private float _stressMax;
        private float _stressCurrent;
        private bool _failState = false;

        private void Start() {
            onStressChange.AddListener(FailState);

            Logger.Log(LogType.Player, $"{typeof(PlayerStress).Name} Initialized");
        }

        public void AddStress(float amount) {
            if (!_failState) {
                _stressCurrent += amount;
                onStressChange.Invoke(_stressCurrent);

                Logger.Log(LogType.Player, $"Stress Meter: {_stressCurrent}/{_stressMax}");
            }
        }

        private void FailState(float stressCurrent) {
            if (!_failState && stressCurrent >= _stressMax) {
                _failState = true;
                onFailState.Invoke();

                Logger.Log(LogType.Player, $"Player Fail State");
            }
        }

        [ContextMenu("AddStressDebug")]
        private void AddStressDebug() {
            AddStress(1);
        }

    }
}