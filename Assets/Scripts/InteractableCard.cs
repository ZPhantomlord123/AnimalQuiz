using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableCard : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private bool isDragging;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 offset;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        SetDraggedPosition(eventData);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            SetDraggedPosition(eventData);
        }
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPointerPosition))
            {
                rectTransform.localPosition = localPointerPosition - offset;
            }
        }
        else
        {
            Vector2 localPointerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPointerPosition);
            rectTransform.localPosition = localPointerPosition - offset;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = true;
    }
}
