using UnityEngine;
using UnityEngine.EventSystems;

public class Pin : MonoBehaviour, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _pressingTime;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private bool _pressingTimeIsCorrect;
    private float _timeHasPassed;

    public void OnDrag(PointerEventData eventData)
    {
        if (_pressingTimeIsCorrect)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
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
}