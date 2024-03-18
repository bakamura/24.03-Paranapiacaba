using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class FusionBox : MonoBehaviour
{
    [SerializeField] private Vector2 _matrixDimensions;
    [SerializeField] private Vector2 _distanceBetweenFuses;
    [SerializeField] private LayerMask _fuseLayer;
    [SerializeField] private Transform _fuseObjectsParent;
    [SerializeField] private CanvasGroup _fuseUIParent;
    [SerializeField] private InputActionReference _changeFuseInput;
    [SerializeField] private InputActionReference _activateFuseInput;
    [SerializeField] private InputActionAsset _inputActionMap;
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private UnityEvent _onInteractionCancelled;
    private MeshRenderer[] _meshRenderers;
    private MeshRenderer _currentSelected;
    private Color _previousColor = Color.white;
    private bool _updateSelected;
    private bool _updateActivated;
    private bool _isActive;    

    private void Awake()
    {
        _changeFuseInput.action.performed += HandleUINavigation;
        _activateFuseInput.action.performed += HandleActivateFuse;

        MeshRenderer[] temp = _fuseObjectsParent.GetComponentsInChildren<MeshRenderer>(false);
        _meshRenderers = new MeshRenderer[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            _meshRenderers[i] = temp[i];
        }
        Setup();
    }

    private void Setup()
    {        
        EventSystem.current.SetSelectedGameObject(_fuseUIParent.GetComponentInChildren<Button>(false).gameObject);
        int index = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        _previousColor = _meshRenderers[index].material.color;
        _currentSelected = _meshRenderers[index];
        _currentSelected.material.color = Color.yellow;
        _isActive = true;
        UpdateInputsAndUI(_isActive);
        _onInteract?.Invoke();
    }

    private void UpdateInputsAndUI(bool isActive)
    {
        if(isActive)
        {
            //gameplay inputs
            _inputActionMap.actionMaps[0].Disable();
            //UI inputs
            _inputActionMap.actionMaps[1].Enable();            
        }
        else
        {
            _inputActionMap.actionMaps[0].Enable();
            _inputActionMap.actionMaps[1].Disable();
        }
        _fuseUIParent.interactable = isActive;
        _fuseUIParent.blocksRaycasts = isActive;
    }

    private void HandleUINavigation(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() != Vector2.zero && EventSystem.current.currentSelectedGameObject)
        {
            _updateSelected = true;
        }
    }

    private void HandleActivateFuse(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1)
        {
            _updateActivated = true;
        }
    }

    private void UpdateActivateFuse()
    {
        if (_updateActivated)
        {
            int index = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
            RaycastHit hit;
            MeshRenderer mesh;
            //up
            if (Physics.Raycast(_meshRenderers[index].transform.position, _meshRenderers[index].transform.up, out hit, _distanceBetweenFuses.y, _fuseLayer))
            {
                mesh = hit.collider.GetComponent<MeshRenderer>();
                mesh.material.color = mesh.material.color == Color.red ? Color.white : Color.red;
            }
            //if (index - _matrixDimensions.y >= 0)
            //_meshRenderers[index - (int)_matrixDimensions.y].material.color = _meshRenderers[index - (int)_matrixDimensions.y].material.color == Color.red ? Color.white : Color.red;
            //down
            if (Physics.Raycast(_meshRenderers[index].transform.position, -_meshRenderers[index].transform.up, out hit, _distanceBetweenFuses.y, _fuseLayer))
            {
                mesh = hit.collider.GetComponent<MeshRenderer>();
                mesh.material.color = mesh.material.color == Color.red ? Color.white : Color.red;
            }
            //if (index + _matrixDimensions.y < _meshRenderers.Length)
            //_meshRenderers[index + (int)_matrixDimensions.y].material.color = _meshRenderers[index + (int)_matrixDimensions.y].material.color == Color.red ? Color.white : Color.red;
            //left
            if (Physics.Raycast(_meshRenderers[index].transform.position, -_meshRenderers[index].transform.right, out hit, _distanceBetweenFuses.x, _fuseLayer))
            {
                mesh = hit.collider.GetComponent<MeshRenderer>();
                mesh.material.color = mesh.material.color == Color.red ? Color.white : Color.red;
            }
            //if (index % _matrixDimensions.y != 0)
            //_meshRenderers[index - 1].material.color = _meshRenderers[index - 1].material.color == Color.red ? Color.white : Color.red;
            //right
            if (Physics.Raycast(_meshRenderers[index].transform.position, _meshRenderers[index].transform.right, out hit, _distanceBetweenFuses.x, _fuseLayer))
            {
                mesh = hit.collider.GetComponent<MeshRenderer>();
                mesh.material.color = mesh.material.color == Color.red ? Color.white : Color.red;
            }
            //if ((index + 1) % _matrixDimensions.y != 0)
            //_meshRenderers[index + 1].material.color = _meshRenderers[index + 1].material.color == Color.red ? Color.white : Color.red;

            _previousColor = _previousColor == Color.red ? Color.white : Color.red;
            _updateActivated = false;
            CheckCompletion();
        }
    }

    private void CheckCompletion()
    {
        for(int i = 0; i < _meshRenderers.Length; i++)
        {
            if (_meshRenderers[i].material.color != Color.red)
                return;
        }
        _isActive = false;
        UpdateInputsAndUI(_isActive);
        //onActivate?.invoke();
    }

    private void UpdateFuseSelected()
    {
        if (_updateSelected)
        {
            //Debug.Log($"parse {Int32.Parse(_eventSystem.currentSelectedGameObject.name)}. name {_eventSystem.currentSelectedGameObject.name}");
            _currentSelected.material.color = _previousColor;
            _currentSelected = _meshRenderers[Int32.Parse(EventSystem.current.currentSelectedGameObject.name)];
            _previousColor = _currentSelected.material.color;
            _currentSelected.material.color = Color.yellow;
            _updateSelected = false;
        }
    }

    #region Utilities
    [ContextMenu("RenameObjects")]
    public void RenameObjects()
    {
        if (_fuseObjectsParent)
        {
            MeshRenderer[] temp = _fuseObjectsParent.GetComponentsInChildren<MeshRenderer>(false);
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].name = $"{i}";
            }
        }
        if (_fuseUIParent)
        {
            Button[] temp = _fuseUIParent.GetComponentsInChildren<Button>(false);
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].name = $"{i}";
            }
        }
    }

    [ContextMenu("RepositionFuses")]
    public void RepositionFuses()
    {
        sbyte currentX = 0;
        sbyte currentY = 0;
        MeshRenderer[] temp = _fuseObjectsParent.GetComponentsInChildren<MeshRenderer>(false);
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].transform.localPosition = new Vector3(currentX * _distanceBetweenFuses.x, currentY * _distanceBetweenFuses.y, 0);
            currentX++;
            if (currentX == _matrixDimensions.x)
            {
                currentX = 0;
                currentY++;
            }
        }
    }
    [ContextMenu("UpdateFusesActiveState")]
    public void UpdateFusesActiveState()
    {
        if (_fuseObjectsParent && _fuseUIParent)
        {
            Button[] uiElements = _fuseUIParent.GetComponentsInChildren<Button>(true);
            MeshRenderer[] objectElements = _fuseObjectsParent.GetComponentsInChildren<MeshRenderer>(true);
            if (uiElements.Length == objectElements.Length)
            {
                for (int i = 0; i < uiElements.Length; i++)
                {
                    objectElements[i].gameObject.SetActive(uiElements[i].gameObject.activeInHierarchy);
                }
            }
        }
    }
    #endregion

    private void OnGUI()
    {
        if(_isActive)
        {
        UpdateFuseSelected();
        UpdateActivateFuse();
        }
    }
}
