using System.Collections.Generic;
using DefaultNamespace;
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
    private GameData _gameData;
    private PinData _selectedPinData;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_preview.gameObject.activeInHierarchy)
        {
            _preview.SetActive(false);
        }
    }

    private void Start()
    {
        _gameData = new();
        _gameData.pins = new();

        _addNewPinButton.onClick.AddListener(AddNewPin);
        _saveAllPinsButton.onClick.AddListener(SaveAllPins);

        _editMenu.SubmitClicked += SavePinData;
        _preview.SeeMoreClicked += OpenMainMenu;
        _preview.EditClicked += OpenEditMenu;
        _preview.DeleteClicked += DeletePin;
    }

    private void AddNewPin()
    {
        PinData data = new PinData();
        Pin newPin = Instantiate(_pinPrefab, transform);

        newPin.Preview = _preview;
        newPin.MainMenu = _mainMenu;
        newPin.BeginDrag += SetActivePreview;
        newPin.Selected += PinSelected;
        newPin.SetData(data);

        _pins.Add(newPin);
    }

    private void PinSelected(PinData data)
    {
        _selectedPinData = data;
    }

    private void SavePinData()
    {
        var title = _editMenu.GetTitle();
        var description = _editMenu.GetDescription();
        //image

        _selectedPinData.title = title;
        _selectedPinData.description = description;
    }

    private void SetActivePreview()
    {
        _preview.SetActive(false);
    }

    private void SaveAllPins()
    {
        Debug.Log("All pins saved");
        //проходимся по всему списку пинов и о каждом сохраняем информацию
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
        //удаляем конкретный пин и информацию о нем
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