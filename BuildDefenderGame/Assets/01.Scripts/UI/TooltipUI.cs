using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance{ get; private set; }
    
    private TextMeshProUGUI _textMeshPro;

    private RectTransform _backgroundRectTransform;

    [SerializeField]
    private RectTransform _canvasRectTransform;

    private RectTransform _rectTransform;

    private TooltipTimer _tooltipTimer;

    private void Awake()
    {
        _textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        _backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();

        _rectTransform = GetComponent<RectTransform>();

        Instance = this;
        SetText("Hello World");
        
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();
        
        if (_tooltipTimer != null)
        {
            _tooltipTimer.timer -= Time.deltaTime;
            if (_tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    private void SetText(string tooltipText)
    {
        _textMeshPro.SetText(tooltipText);
        _textMeshPro.ForceMeshUpdate();
        
        // false를 적는것과 아예 파라미터를 안적는 것이 차이가 크다
        Vector2 textSize = _textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        _backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string tooltipText, TooltipTimer timer = null)
    {
        _tooltipTimer = timer;
        gameObject.SetActive(true);
        SetText(tooltipText: tooltipText);
        HandleFollowMouse();
    }
    public class TooltipTimer
    {
        public float timer;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition/ _canvasRectTransform.localScale.x;

        if(anchoredPosition.x + _backgroundRectTransform.rect.width > _canvasRectTransform.rect.width)
        {
            anchoredPosition.x = _canvasRectTransform.rect.width - _backgroundRectTransform.rect.width;
        }
        
        if(anchoredPosition.y + _backgroundRectTransform.rect.height > _canvasRectTransform.rect.height)
        {
            anchoredPosition.y = _canvasRectTransform.rect.height - _backgroundRectTransform.rect.height;
        }
        
        if(anchoredPosition.x <= 0)
        {
            anchoredPosition.x = 0;
        }
        
        if(anchoredPosition.y <= 0)
        {
            anchoredPosition.y = 0;
        }
        
        _rectTransform.anchoredPosition = anchoredPosition;
    }
}
