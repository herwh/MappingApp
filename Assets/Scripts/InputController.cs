using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _pressingTime;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private Vector2 _startPosition;
    private bool _pressingTimeIsCorrect;
    private float _timeHasPassed;

    private float _yLimitPosition;
    private float _xLimitPosition;

    public void OnDrag(PointerEventData eventData)
    {
        if (_pressingTimeIsCorrect)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CheckPinPosition();
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
        var widthCanvas = Screen.width / _canvas.scaleFactor;
        var heightCanvas = Screen.height / _canvas.scaleFactor;

        _yLimitPosition = heightCanvas / 2;
        _xLimitPosition = widthCanvas / 2;
    }
}
