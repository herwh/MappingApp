using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _pressingTime;

    protected RectTransform _rectTransform;
    protected float _canvasWidth;
    protected float _canvasHeight;
    protected Canvas _canvas;
    protected bool _isDragging;
    
    private Vector2 _startPosition;
    private bool _pressingTimeIsCorrect;
    private float _timeHasPassed;
    private float _yLimitPosition;
    private float _xLimitPosition;

    public void OnDrag(PointerEventData eventData)
    {
        _isDragging = true;
        
        if (_pressingTimeIsCorrect)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CheckPinPosition();
        _isDragging = false;
    }

    private void Awake()
    {
        _startPosition = gameObject.transform.position;
        _canvas = FindObjectOfType<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        
        GetScreenPoints();
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
        Vector2 currentPinPosition = _rectTransform.anchoredPosition;

        if (currentPinPosition.y > _yLimitPosition || currentPinPosition.y < -_yLimitPosition ||
            currentPinPosition.x > _xLimitPosition || currentPinPosition.x < -_xLimitPosition)
        {
            gameObject.transform.position = _startPosition;
        }
    }

    private void GetScreenPoints()
    {
        _canvasWidth = Screen.width / _canvas.scaleFactor;
        _canvasHeight = Screen.height / _canvas.scaleFactor;

        _yLimitPosition = _canvasHeight / 2;
        _xLimitPosition = _canvasWidth / 2;
    }
}
