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
    [SerializeField] private Canvas _canvas;

    private List<Pin> _pins = new();
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
        data.id = _pins.Count;
        Pin newPin = Instantiate(_pinPrefab, transform);
        
        newPin.BeginDrag += SetActivePreview;
        newPin.Selected += PinSelected;
        newPin.SetData(data);

        _pins.Add(newPin);
    }

    private void PinSelected(PinData data)
    {
        _selectedPinData = data;

        if (_pins[data.id].IsDragging)
        {
            _preview.gameObject.SetActive(false);
        }
        else
        {
            _preview.gameObject.SetActive(true);
            _preview.SetContent(data.title, null); //IMAGE
        }

        SetPreviewPosition(_pins[data.id].RectTransform);
    }
    
    private void SavePinData()
    {
        var title = _editMenu.GetTitle();
        var description = _editMenu.GetDescription();
        //image

        _selectedPinData.title = title;
        _selectedPinData.description = description;
        //image
    }

    private void SetPreviewPosition(RectTransform rectTransform)
    {
        var position = rectTransform.anchoredPosition;
        var size = rectTransform.rect.size;
        var previewSize = _preview.GetPreviewSize();
        var (canvasWidth, canvasHeight) = _canvas.GetCanvasSize();
        var distanceToBottom = Mathf.Abs(position.y + canvasHeight / 2); //расстояние от пина до нижней точки
        var distanceToLeft = Mathf.Abs(position.x + canvasWidth / 2); //расстояние от пина до левой точки
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
        Debug.Log("All pins saved");
        //проходимся по всему списку пинов и о каждом сохраняем информацию
    }

    private void OpenMainMenu()
    {
        _mainMenu.gameObject.SetActive(true);
        _preview.gameObject.SetActive(false);
        
        _mainMenu.SetContent(_selectedPinData.title,_selectedPinData.description,null); //IMAGE
    }

    private void OpenEditMenu()
    {
        _editMenu.gameObject.SetActive(true);
        _preview.gameObject.SetActive(false);
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