using UnityEngine;
using UnityEngine.UI;

public class Pin : DraggableObject
{
    [SerializeField] private Button _button;

    public PreviewViewController Preview { get; set; }

    private void Start()
    {
        _button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        Preview.gameObject.SetActive(true);
        CheckPinPosition();
    }

    private void CheckPinPosition()
    {
        var position = _rectTransform.anchoredPosition;//позиция пина
        var size = new Vector2(_rectTransform.rect.x, _rectTransform.rect.y);//размер пина
        var previewSize = Preview.GetPreviewSize();//размер превью
        var distanceToTop = _canvasHeight - position.y;//расстояние пина до верхней точки
        var newPreviewPosition = new Vector2();
        
        if (previewSize.y > distanceToTop)
        {
            newPreviewPosition.x=_rectTransform.anchoredPosition.x;
            newPreviewPosition.y = position.y - size.y;
            
            Preview.SetCorrectPosition(newPreviewPosition);
        }

    }

    private void OnDestroy()
    {
        Preview.gameObject.SetActive(false);
        _button.onClick.RemoveListener(ButtonClicked);
    }
}