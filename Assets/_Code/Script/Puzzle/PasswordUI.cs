using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Ivayami.Puzzle
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PasswordUI : MonoBehaviour
    {
        [SerializeField] protected string password;
        [HideInInspector] public Action OnCheckPassword;
        //[SerializeField] protected UnityEvent onPasswordCorrect;
        //[SerializeField] protected UnityEvent onPasswordWrong;
        [SerializeField] private GameObject _initialSelected;

        private CanvasGroup _canvasGroup;
        //private Transform _container;

        public GameObject FallbackButton => _initialSelected;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            //_container = transform.Find("Container");
        }
        public abstract bool CheckPassword();

        public virtual void UpdateActiveState(bool isActive)
        {
            //if (_canvasGroup)
            //{
            _canvasGroup.alpha = isActive ? 1 : 0;
            _canvasGroup.interactable = isActive;
            _canvasGroup.blocksRaycasts = isActive;
            //_container.gameObject.SetActive(isActive);
            //}
            //else
            //{
            //    _container.gameObject.SetActive(isActive);
            //}
            if (isActive) EventSystem.current.SetSelectedGameObject(_initialSelected);
        }
    }
}