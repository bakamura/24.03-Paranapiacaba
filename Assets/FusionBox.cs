using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class FusionBox : MonoBehaviour
{
    [SerializeField] private Vector2 _matrixDimensions;
    [SerializeField] private GameObject _fuseObjectsParent;
    [SerializeField] private GameObject _fuseUIParent;
    [SerializeField] private InputActionReference _changeFuseInput;
    [SerializeField] private InputActionReference _activateFuseInput;
    private MeshRenderer[] _meshRenderers;
    private EventSystem _eventSystem;
    private MeshRenderer _currentSelected;
    private Color _previousColor = Color.white;
    private bool _updateSelected;
    private bool _updateActivated;

    private void Awake()
    {        
        _eventSystem = FindObjectOfType<EventSystem>();
        _changeFuseInput.action.performed += HandleUINavigation;
        _activateFuseInput.action.performed += HandleActivateFuse;

        MeshRenderer[] temp = _fuseObjectsParent.GetComponentsInChildren<MeshRenderer>();
        _meshRenderers = new MeshRenderer[temp.Length];
        for(int i = 0; i < temp.Length; i++)
        {
            _meshRenderers[i] = temp[i];
        }
        _eventSystem.SetSelectedGameObject(_fuseUIParent.GetComponentInChildren<Button>().gameObject);
        _previousColor = _meshRenderers[0].material.color;
        _currentSelected = _meshRenderers[0];
        _currentSelected.material.color = Color.yellow;
    }

    private void HandleUINavigation(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() != Vector2.zero && _eventSystem.currentSelectedGameObject) 
        {
            _updateSelected = true;
        }
    }

    private void HandleActivateFuse(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() == 1)
        {
            _updateActivated = true;            
        }
    }

    private void UpdateActivateFuse()
    {
        if (_updateActivated)
        {
            int index = Int32.Parse(_eventSystem.currentSelectedGameObject.name);
            //up
            if(index - _matrixDimensions.y > 0)
                _meshRenderers[index - (int)_matrixDimensions.y].material.color = _meshRenderers[index - (int)_matrixDimensions.y].material.color == Color.red ? Color.white : Color.red;
            //down
            if (index + _matrixDimensions.y < _meshRenderers.Length) 
                _meshRenderers[index + (int)_matrixDimensions.y].material.color = _meshRenderers[index + (int)_matrixDimensions.y].material.color == Color.red ? Color.white : Color.red;
            //left
            if (index % _matrixDimensions.y != 0) 
                _meshRenderers[index - 1].material.color = _meshRenderers[index - 1].material.color == Color.red ? Color.white : Color.red;
            //right
            if (index + 1 % _matrixDimensions.x != 0) 
                _meshRenderers[index + 1].material.color = _meshRenderers[index + 1].material.color == Color.red ? Color.white : Color.red;

            _previousColor = _previousColor == Color.red ? Color.white : Color.red;
            _updateActivated = false;
        }
    }

    private void UpdateFuseSelected()
    {
        if (_updateSelected)
        {
            //Debug.Log($"parse {Int32.Parse(_eventSystem.currentSelectedGameObject.name)}. name {_eventSystem.currentSelectedGameObject.name}");
            _currentSelected.material.color = _previousColor;
            _currentSelected = _meshRenderers[Int32.Parse(_eventSystem.currentSelectedGameObject.name)];
            _previousColor = _currentSelected.material.color;
            _currentSelected.material.color = Color.yellow;
            _updateSelected = false;
        }
    }

    private void OnGUI()
    {
        UpdateActivateFuse();
        UpdateFuseSelected();
    }
}
