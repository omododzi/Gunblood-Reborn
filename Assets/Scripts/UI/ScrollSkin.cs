using UnityEngine;
using System.Collections;

public class ScrollSkin : MonoBehaviour
{
    public float scrollSpeed = 100f;
    public float smoothTime = 0.3f;
    private float startY;
    public float endY;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private Vector2 currentVelocity;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        startY = parentRectTransform.anchoredPosition.y;
        endY = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
    }

    private void OnDisable()
    {
        Vector2 newPos = new Vector2(rectTransform.anchoredPosition.x, startY);
        rectTransform.anchoredPosition = newPos;
    }

    private void OnEnable()
    {
        StartCoroutine(SmoothScroll());
    }

    private IEnumerator SmoothScroll()
    {
        Vector2 startPos = new Vector2(rectTransform.anchoredPosition.x, startY);
        Vector2 targetPos = new Vector2(rectTransform.anchoredPosition.x, endY);
        rectTransform.anchoredPosition = startPos;

        float elapsedTime = 0f;
        
        while (elapsedTime < smoothTime)
        {
            rectTransform.anchoredPosition = Vector2.SmoothDamp(
                rectTransform.anchoredPosition,
                targetPos,
                ref currentVelocity,
                smoothTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Гарантируем точное попадание в конечную позицию
        rectTransform.anchoredPosition = targetPos;
    }
}