using System;
using UnityEngine;
using UnityEngine.UI;

public class PreviewViewController : MonoBehaviour
{
    [SerializeField] private Button _seeMoreButton;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;

    public event Action SeeMoreClicked;
    public event Action EditClicked;
    public event Action DeleteClicked;
    
    private RectTransform _rectTransform;

    public Vector2 GetPreviewSize()
    {
        return new Vector2(_rectTransform.rect.size.x, _rectTransform.rect.size.y);
    }

    public void SetCorrectPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = new Vector2(position.x, position.y);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
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
        if (SeeMoreClicked != null) SeeMoreClicked();
    }

    private void EditButtonClicked()
    {
        if (EditClicked != null) EditClicked();
    }

    private void DeleteButtonClicked()
    {
        if (DeleteClicked != null) DeleteClicked();
       
    }

    private void OnDestroy()
    {
        _seeMoreButton.onClick.RemoveListener(SeeMoreButtonClicked);
        _editButton.onClick.RemoveListener(EditButtonClicked);
        _deleteButton.onClick.RemoveListener(DeleteButtonClicked);
    }
}