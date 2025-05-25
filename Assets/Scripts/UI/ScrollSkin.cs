using UnityEngine;
using System.Collections;

public class ScrollSkin : MonoBehaviour
{
    public float scrollSpeed = 100f;
    public float smoothTime = 0.5f;
    public AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    private float startY;
    private float startX;
    public float endX;
    public float endY;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private Coroutine currentCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        startY = parentRectTransform.anchoredPosition.y;
        startX = parentRectTransform.anchoredPosition.x;
        
        if (endX == 0 && endY == 0)
        {
            endX = rectTransform.anchoredPosition.x;
            endY = rectTransform.anchoredPosition.y;
        }
    }

    private void OnDisable()
    {
        // Останавливаем все корутины при деактивации
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        
        // Немедленный сброс позиции без корутины
        rectTransform.anchoredPosition = new Vector2(startX, startY);
    }

    private void OnEnable()
    {
        // Запускаем новую корутину только если объект активен
        if (gameObject.activeInHierarchy)
        {
            currentCoroutine = StartCoroutine(SmoothScroll());
        }
    }

    private IEnumerator SmoothScroll()
    {
        Vector2 startPos = new Vector2(startX, startY);
        Vector2 targetPos = new Vector2(endX, endY);
        
        rectTransform.anchoredPosition = startPos;
        
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime / smoothTime;
            
            rectTransform.anchoredPosition = Vector2.Lerp(
                startPos, 
                targetPos, 
                easeCurve.Evaluate(t));
            
            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
        currentCoroutine = null;
    }
}