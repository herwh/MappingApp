using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    [SerializeField] private float _pressingTime;

    public event Action BeginDrag;
    public bool IsDragging;
    public RectTransform RectTransform;
    
    private Canvas _canvas;
    private Vector2 _startPosition;
    private bool _pressingTimeIsCorrect;
    private float _timeHasPassed;

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        if (BeginDrag != null) BeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        IsDragging = true;

        if (_pressingTimeIsCorrect)
        {
            RectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CheckPinPosition();
        IsDragging = false;
    }

    private void Awake()
    {
        _startPosition = gameObject.transform.position;
        _canvas = FindObjectOfType<Canvas>();
        RectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        ProcessInput();
        CheckPressingTime();
    }

    private void CheckPressingTime()
    {
        if (_timeHasPassed > _pressingTime)
        {
            _pressingTimeIsCorrect = true;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _pressingTimeIsCorrect = false;
            _timeHasPassed = 0;
        }

        if (Input.GetMouseButton(0))
        {
            _timeHasPassed += Time.deltaTime;
        }
    }

    private void CheckPinPosition()
    {
        Vector2 currentPinPosition = RectTransform.anchoredPosition;

        var (canvasWidth, canvasHeight) = _canvas.GetCanvasSize();
        var yLimitPosition = canvasHeight / 2;
        var xLimitPosition = canvasWidth / 2;
        
        if (currentPinPosition.y > yLimitPosition || currentPinPosition.y < -yLimitPosition ||
            currentPinPosition.x > xLimitPosition || currentPinPosition.x < -xLimitPosition)
        {
            gameObject.transform.position = _startPosition;
        }
    }
}
