using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ScrollSkin : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public float smoothTime = 0.5f;
    public AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public List<GameObject> summ;
    public GameObject loced;
    private bool isopen = false;
    
    
    private float startY;
    private float startX;
    public float endX;
    public float endY;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private Coroutine currentCoroutine;
    
    private void Awake()
    {
        if (isopen&& loced!=null)
        {
            loced.SetActive(false);
        }
        else if (!isopen&&loced!=null)
        {
            loced.SetActive(true);
        }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isopen && summ != null)
        {
            for (int i = 0; i < summ.Count; i++)
            {
                summ[i].SetActive(true);
            }
        }
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (summ != null)
        {
            for (int i = 0; i < summ.Count; i++)
            {
                summ[i].SetActive(false);
            }
        }
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ScoreController.score >= summ.Count && !isopen&& loced!=null)
        {
            loced.SetActive(false);
            isopen = true;
            ScoreController.score -= summ.Count;
        }
    }
    
}