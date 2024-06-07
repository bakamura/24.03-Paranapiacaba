using UnityEngine;
using FMODUnity;
using FMOD.Studio;

namespace Ivayami.Audio
{
    public class InteractableSounds : EntitySound
    {
        [SerializeField] private bool _debugLog;
        [SerializeField] private EventReference _interactSoundReference;
        [SerializeField] private EventReference _interactReturnSoundReference;
        [SerializeField] private EventReference _activateSoundReference;
        [SerializeField] private EventReference _activateReturnSoundReference;
        [SerializeField] private EventReference _collectSoundReference;
        [SerializeField] private EventReference _actionFailedSoundReference;

        private EventInstance _interactSoundInstance;
        private EventInstance _interactReturnSoundInstance;
        private EventInstance _activateSoundInstance;
        private EventInstance _activateReturnSoundInstance;
        private EventInstance _collectSoundInstance;
        private EventInstance _actionFailedSoundInstance;

        private bool _hasDoneSetup;

        public enum SoundTypes
        {
            Interact,
            InteractReturn,
            Activate,
            ActivateReturn,
            Collect,
            ActionFailed
        }

        public void PlaySound(SoundTypes soundType)
        {
            Setup();
            if (_debugLog) Debug.Log($"Interactable {name} is playing soun {soundType}");
            switch (soundType)
            {
                case SoundTypes.Interact:
                    PlayOneShot(_interactSoundInstance);
                    break;
                case SoundTypes.InteractReturn:
                    PlayOneShot(_interactReturnSoundInstance);
                    break;
                case SoundTypes.Activate:
                    PlayOneShot(_activateSoundInstance);
                    break;
                case SoundTypes.ActivateReturn:
                    PlayOneShot(_activateReturnSoundInstance);
                    break;
                case SoundTypes.Collect:
                    PlayOneShot(_collectSoundInstance);
                    break;
                case SoundTypes.ActionFailed:
                    PlayOneShot(_actionFailedSoundInstance);
                    break;
            }
        }

        private void Setup()
        {
            if (!_hasDoneSetup)
            {
                if(!_interactSoundReference.IsNull)_interactSoundInstance = InstantiateEvent(_interactSoundReference);
                if (!_interactReturnSoundReference.IsNull) _interactReturnSoundInstance = InstantiateEvent(_interactReturnSoundReference);
                if (!_activateSoundReference.IsNull) _activateSoundInstance = InstantiateEvent(_activateSoundReference);
                if (!_activateReturnSoundReference.IsNull) _activateReturnSoundInstance = InstantiateEvent(_activateReturnSoundReference);
                if (!_collectSoundReference.IsNull) _collectSoundInstance = InstantiateEvent(_collectSoundReference);
                if (!_actionFailedSoundReference.IsNull) _actionFailedSoundInstance = InstantiateEvent(_actionFailedSoundReference);
                _hasDoneSetup = true;
            }
        }
    }
}