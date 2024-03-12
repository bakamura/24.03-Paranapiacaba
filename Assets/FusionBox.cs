using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class FusionBox : MonoBehaviour
{
    [SerializeField] private GameObject _fuseObjectsParent;
    [SerializeField] private GameObject _fuseUIParent;
    [SerializeField] private InputActionReference _changeFuseInput;
    private List<int> _fuseUIIDs = new List<int>();
    private List<MeshRenderer> _meshRenderers = new List<MeshRenderer>();
    private EventSystem _eventSystem;
    private MeshRenderer _currentSelected;

    private void Awake()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        _changeFuseInput.action.performed += HandleUINavigation;
        foreach(Button btn in _fuseUIParent.GetComponentsInChildren<Button>())
        {
            _fuseUIIDs.Add(btn.gameObject.GetInstanceID());            
        }
        foreach(MeshRenderer meshRenderer in _fuseObjectsParent.GetComponentsInChildren<MeshRenderer>())
        {
            _meshRenderers.Add(meshRenderer);
        }
        _eventSystem.SetSelectedGameObject(_fuseUIParent.GetComponentInChildren<Button>().gameObject);
        _currentSelected = _meshRenderers[_fuseUIIDs.IndexOf(_eventSystem.currentSelectedGameObject.GetInstanceID())];
        _currentSelected.material.color = Color.red;
    }

    private void HandleUINavigation(InputAction.CallbackContext context)
    {
        if(context.ReadValue<Vector2>() != Vector2.zero && _eventSystem.currentSelectedGameObject) 
        {
            _currentSelected.material.color = Color.white;
            Debug.Log($"index is {_fuseUIIDs.IndexOf(_eventSystem.currentSelectedGameObject.GetInstanceID())}");
            _currentSelected = _meshRenderers[_fuseUIIDs.IndexOf(_eventSystem.currentSelectedGameObject.GetInstanceID())];
            _currentSelected.material.color = Color.red;
        }
    }
}
