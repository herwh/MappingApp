using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Pin : DraggableObject
{
    [SerializeField] private Button _button;
    public PreviewViewController Preview { get; set; }
    public MainMenuViewController MainMenu { get; set; }

    public event Action<PinData> Selected= delegate {  };
    
    private PinData _data;
    private const float HALF = 0.5f;

    public void SetData(PinData data)
    {
        _data = data;
    }
    
    private void Start()
    {
        _button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        Selected(_data);
        
        if (_isDragging)
        {
            Preview.SetActive(false);
        }
        else
        {
           Preview.SetActive(true); 
        }

        SetPreviewPosition();
    }

    private void SetPreviewPosition()
    {
        var position = _rectTransform.anchoredPosition; 
        var size = _rectTransform.rect.size;
        var previewSize = Preview.GetPreviewSize();
        var distanceToBottom = Mathf.Abs(position.y + _canvasHeight / 2); //расстояние от пина до нижней точки
        var distanceToLeft = Mathf.Abs(position.x + _canvasWidth / 2); //расстояние от пина до левой точки
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

        Preview.SetCorrectPosition(newPreviewPosition);
    }
    
    

    private void OnDestroy()
    {
        Preview.gameObject.SetActive(false);
        _button.onClick.RemoveListener(ButtonClicked);
    }
}