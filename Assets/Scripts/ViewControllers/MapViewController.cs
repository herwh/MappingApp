using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
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
    [SerializeField] private Canvas _canvas;

    private readonly List<PinData> _deletedPins = new();
    private readonly Dictionary<int, Pin> _pins = new();
    private GameData _gameData;
    private PinData _selectedPinData;

    private const float HALF = 0.5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_preview.gameObject.activeInHierarchy)
        {
            _preview.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        _gameData = SaveLoadUtility.LoadGame();
        CreatePins();

        _addNewPinButton.onClick.AddListener(AddNewPin);
        _saveAllPinsButton.onClick.AddListener(SaveAllPins);

        _editMenu.SubmitClicked += SavePinData;
        _preview.SeeMoreClicked += OpenMainMenu;
        _preview.EditClicked += OpenEditMenu;
        _preview.DeleteClicked += DeletePin;
    }

    private void CreatePins()
    {
        _gameData.pins ??= new List<PinData>();

        foreach (var pin in _gameData.pins)
        {
            CreateNewPin(pin);
        }
    }

    private void AddNewPin()
    {
        PinData data = new PinData();
        data.id = GUID.Generate().GetHashCode();
        CreateNewPin(data);
        _gameData.pins.Add(data);
    }

    private void CreateNewPin(PinData data)
    {
        Pin newPin = Instantiate(_pinPrefab, transform);

        newPin.BeginDrag += SetActivePreview;
        newPin.Selected += PinSelected;
        newPin.SetData(data);

        _pins.Add(data.id, newPin);
    }

    private void PinSelected(PinData data)
    {
        _selectedPinData = data;
        var selectedPin = _pins[data.id];
        var anchoredPosition = selectedPin.RectTransform.anchoredPosition;
        _selectedPinData.pinPosition = new PinPosition(anchoredPosition.x, anchoredPosition.y);

        if (selectedPin.IsDragging)
        {
            _preview.gameObject.SetActive(false);
        }
        else
        {
            _preview.gameObject.SetActive(true);
            _preview.SetContent(data.title, SaveLoadUtility.LoadImage(data.imagePath));
        }

        SetPreviewPosition(selectedPin.RectTransform);
    }

    private void SavePinData()
    {
        var title = _editMenu.GetTitle();
        var description = _editMenu.GetDescription();
        var imagePath = _editMenu.GetImagePath();

        _selectedPinData.title = title;
        _selectedPinData.description = description;
        _selectedPinData.imagePath = imagePath;
    }

    private void SetPreviewPosition(RectTransform rectTransform)
    {
        var position = rectTransform.anchoredPosition;
        var size = rectTransform.rect.size;
        var previewSize = _preview.GetPreviewSize();
        var (canvasWidth, canvasHeight) = _canvas.GetCanvasSize();
        var distanceToBottom = Mathf.Abs(position.y + canvasHeight / 2);
        var distanceToLeft = Mathf.Abs(position.x + canvasWidth / 2);
        var newPreviewPosition = position;

        newPreviewPosition.y -= previewSize.y * HALF + size.y * HALF;
        newPreviewPosition.x -= previewSize.x * HALF + size.x * HALF;

        if (previewSize.y > distanceToBottom)
        {
            newPreviewPosition.y += previewSize.y;
        }

        if (previewSize.x > distanceToLeft)
        {
            newPreviewPosition.x += previewSize.x + size.x;
        }

        _preview.SetCorrectPosition(newPreviewPosition);
    }

    private void SetActivePreview()
    {
        _preview.gameObject.SetActive(false);
    }

    private void SaveAllPins()
    {
        foreach (var pin in _deletedPins)
        {
            SaveLoadUtility.DeleteImage(pin.imagePath);
        }

        SaveLoadUtility.SaveGame(_gameData);
    }

    private void OpenMainMenu()
    {
        _mainMenu.gameObject.SetActive(true);
        _preview.gameObject.SetActive(false);

        _mainMenu.SetContent(_selectedPinData.title, _selectedPinData.description,
            SaveLoadUtility.LoadImage(_selectedPinData.imagePath));
    }

    private void OpenEditMenu()
    {
        _editMenu.gameObject.SetActive(true);
        _preview.gameObject.SetActive(false);
    }

    private void DeletePin()
    {
        int pinID = _selectedPinData.id;
        Pin pin = _pins[pinID];

        Destroy(pin.gameObject);
        _preview.gameObject.SetActive(false);

        _pins.Remove(pinID);
        _gameData.pins.Remove(_selectedPinData);
        _deletedPins.Add(_selectedPinData);
        _selectedPinData = null;

    }

    private void OnDestroy()
    {
        _addNewPinButton.onClick.RemoveListener(AddNewPin);
        _saveAllPinsButton.onClick.RemoveListener(SaveAllPins);

        _preview.SeeMoreClicked -= OpenMainMenu;
        _preview.EditClicked -= OpenEditMenu;
        _preview.DeleteClicked -= DeletePin;

        foreach (var pin in _pins.Values)
        {
            pin.BeginDrag -= SetActivePreview;
        }
    }
}