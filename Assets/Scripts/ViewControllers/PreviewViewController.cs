using System;
using UnityEngine;
using UnityEngine.UI;

public class PreviewViewController : MonoBehaviour
{
    [SerializeField] private Button _seeMoreButton;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;

    public event Action SeeMoreClicked = delegate { };
    public event Action EditClicked = delegate { };
    public event Action DeleteClicked = delegate { };

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
        SeeMoreClicked();
    }

    private void EditButtonClicked()
    {
        EditClicked();
    }

    private void DeleteButtonClicked()
    {
        DeleteClicked();
        //скорее всего через событие и Map удаляем конкретный пин
        //+нужно будет иметь список всех пинов
    }

    private void OnDestroy()
    {
        _seeMoreButton.onClick.RemoveListener(SeeMoreButtonClicked);
        _editButton.onClick.RemoveListener(EditButtonClicked);
        _deleteButton.onClick.RemoveListener(DeleteButtonClicked);
    }
}