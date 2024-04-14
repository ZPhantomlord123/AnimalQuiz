using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableCard : MonoBehaviour, IBeginDragHandler ,IEndDragHandler,IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 offset;
    private Vector2 originalPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        SnapBack();
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPointerPosition);
        rectTransform.localPosition = localPointerPosition - offset;
    }

    public void DisableCard()
    {
        this.gameObject.SetActive(false);
    }

    public void SnapBack()
    {
        rectTransform.anchoredPosition = originalPosition;
    }
}
