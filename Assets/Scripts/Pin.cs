using System;
using UnityEngine;
using UnityEngine.UI;

public class Pin : DraggableObject
{
    [SerializeField] private Button _button;

    public event Action<PinData> Selected = delegate { };

    private PinData _data;

    public void SetData(PinData data)
    {
        _data = data;
        RectTransform.anchoredPosition = new Vector2(_data.pinPosition.x, _data.pinPosition.y);
    }

    private void Start()
    {
        _button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        Selected(_data);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ButtonClicked);
    }
}