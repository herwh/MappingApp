using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapViewController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Button _addNewPinButton;
    [SerializeField] private Button _saveAllPinsButton;
    [SerializeField] private Pin _pinPrefab;
    [SerializeField] private PreviewViewController _preview;
    [SerializeField] private MainMenuViewController _mainMenu;
    [SerializeField] private EditMenuViewController _editMenu;

    private List<Pin> _pins = new();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_preview.gameObject.activeInHierarchy)
        {
            _preview.SetActive(false);
        }
    }

    private void Start()
    {
        _addNewPinButton.onClick.AddListener(AddNewPin);
        _saveAllPinsButton.onClick.AddListener(SaveAllPins);

        _preview.SeeMoreClicked += OpenMainMenu;
        _preview.EditClicked += OpenEditMenu;
        _preview.DeleteClicked += DeletePin;
    }

    private void AddNewPin()
    {
        Pin newPin = Instantiate(_pinPrefab, transform);
        newPin.Preview = _preview;
        newPin.BeginDrag += SetActivePreview;
        _pins.Add(newPin);
    }

    private void SetActivePreview()
    {
        _preview.SetActive(false);
    }

    private void SaveAllPins()
    {
        Debug.Log("All pins saved");
        
    }

    private void OpenMainMenu()
    {
        _mainMenu.SetActive(true);
    }

    private void OpenEditMenu()
    {
        _editMenu.gameObject.SetActive(true);
    }

    private void DeletePin()
    {
        Debug.Log("Delete action");
    }

    private void OnDestroy()
    {
        _addNewPinButton.onClick.RemoveListener(AddNewPin);
        _saveAllPinsButton.onClick.RemoveListener(SaveAllPins);

        _preview.SeeMoreClicked -= OpenMainMenu;
        _preview.EditClicked -= OpenEditMenu;
        _preview.DeleteClicked -= DeletePin;

        foreach (var pin in _pins)
        {
            pin.BeginDrag -= SetActivePreview;
        }
    }
}