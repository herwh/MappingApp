using UnityEngine;
using UnityEngine.UI;

public class Pin : DraggableObject
{
    [SerializeField] private Button _button;
    public PreviewViewController Preview { get; set; }

    private const float HALF = 0.5f;
    private void Start()
    {
        _button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        if (_isDragging)
        {
            return;
        }
        
        Preview.SetActive(true);
        SetPreviewPosition();
    }

    private void SetPreviewPosition()
    {
        var position = _rectTransform.anchoredPosition; //позиция пина
        var size = _rectTransform.rect.size; //размер пина
        var previewSize = Preview.GetPreviewSize(); //размер превью
        var distanceToBottom = Mathf.Abs(position.y + _canvasHeight / 2); //расстояние пина до нижней точки
        var distanceToLeft = Mathf.Abs(position.x + _canvasWidth / 2); //расстояние пина до левой точки
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