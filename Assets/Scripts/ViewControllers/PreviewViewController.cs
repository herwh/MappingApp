using UnityEngine;
using UnityEngine.UI;

public class PreviewViewController : MonoBehaviour
{
    [SerializeField] private Button _seeMoreButton;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;

    private RectTransform _rectTransform;

    public Vector2 GetPreviewSize()
    {
        Vector2 size = new Vector2(_rectTransform.rect.size.x, _rectTransform.rect.size.y);
        
        return size;
    }

    public void SetCorrectPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = new Vector2(position.x, position.y);
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _seeMoreButton.onClick.AddListener(SeeMoreButtonClicked);
        _editButton.onClick.AddListener(EditButtonClicked);
        _deleteButton.onClick.AddListener(DeleteButtonClicked);
    }

    private void SeeMoreButtonClicked()
    {
        
    }

    private void EditButtonClicked()
    {
       
    }

    private void DeleteButtonClicked()
    {
        
    }

    private void OnDestroy()
    {
        _seeMoreButton.onClick.RemoveListener(SeeMoreButtonClicked);
        _editButton.onClick.RemoveListener(EditButtonClicked);
        _deleteButton.onClick.RemoveListener(DeleteButtonClicked);
    }
}