using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform button;
    public float sizeOnHover = 1.1f;
    public float sizeStandart = 1.1f;
    public float animTime = 0.2f;

    void Start()
    {
        button = gameObject.GetComponent<RectTransform>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.DOScale(sizeOnHover * sizeStandart * Vector3.one, animTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.DOScale(sizeStandart * Vector3.one, animTime);
    }

    private void OnDisable()
    {
        button.localScale = sizeStandart * Vector3.one;
    }
}
