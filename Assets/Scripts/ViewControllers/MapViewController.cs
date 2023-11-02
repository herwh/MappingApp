using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapViewController : MonoBehaviour
{
    [SerializeField] private Button _addNewPinButton;
    [SerializeField] private Button _saveAllPinsButton;
    [SerializeField] private Pin _pinPrefab;
    [SerializeField] private PreviewViewController _preview;
    [SerializeField] private MainMenuViewController _mainMenu;
    [SerializeField] private EditMenuViewController _editMenu;

    private List<Pin> _pins = new();

    private void Start()
    {
        _addNewPinButton.onClick.AddListener(AddNewPin);
        _saveAllPinsButton.onClick.AddListener(SaveAllPins);
    }

    private void AddNewPin()
    {
        Pin newPin = Instantiate(_pinPrefab, transform);
        newPin.Preview = _preview;
        _pins.Add(newPin);
    }

    private void SaveAllPins()
    {
        //проходимся по всему списку пинов и о каждом сохраняем информацию
    }

    private void OnDestroy()
    {
        _addNewPinButton.onClick.RemoveListener(AddNewPin);
        _saveAllPinsButton.onClick.RemoveListener(SaveAllPins);
    }
}