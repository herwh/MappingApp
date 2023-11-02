using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pin : MonoBehaviour,  IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private float _pressingTime;

    private RectTransform _rectTransform;
    private Vector2 _size;
    private ICanvasHolder _canvasHolder;
    private bool _pressingTimeIsCorrect;
    private float _timeHasPassed;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        var rect = _rectTransform.rect;
        _size= new Vector2(rect.width, rect.height);
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

            if (_pressingTimeIsCorrect)
            {
                //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y,0);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvasHolder.Canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}