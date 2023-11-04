using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewViewController : MonoBehaviour
{
    [SerializeField] private Button _seeMoreButton;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private RawImage _image;

    public event Action SeeMoreClicked = delegate { };
    public event Action EditClicked = delegate { };
    public event Action DeleteClicked = delegate { };

    public Vector2 GetPreviewSize()
    {
        var rect = _rectTransform.rect;
        return new Vector2(rect.size.x, rect.size.y);
    }

    public void SetCorrectPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = new Vector2(position.x, position.y);
    }

    public void SetContent(string title, Texture image)
    {
        _title.text = title;
        _image.texture = image;
    }

    private void Awake()
    {
        _seeMoreButton.onClick.AddListener(SeeMoreButtonClicked);
        _editButton.onClick.AddListener(EditButtonClicked);
        _deleteButton.onClick.AddListener(DeleteButtonClicked);
    }

    private void SeeMoreButtonClicked()
    {
        SeeMoreClicked();
    }

    private void EditButtonClicked()
    {
        EditClicked();
    }

    private void DeleteButtonClicked()
    {
        DeleteClicked();
    }

    private void OnDestroy()
    {
        _seeMoreButton.onClick.RemoveListener(SeeMoreButtonClicked);
        _editButton.onClick.RemoveListener(EditButtonClicked);
        _deleteButton.onClick.RemoveListener(DeleteButtonClicked);
    }
}