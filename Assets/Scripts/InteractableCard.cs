using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableCard : MonoBehaviour, IBeginDragHandler ,IEndDragHandler,IDragHandler, IPointerClickHandler
{
    public RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 offset;
    private Vector2 originalPosition;

    public CardCategory[] categories;
    public string animalNameText;
    public string descriptionText;

    public GameObject correctContext;
    public GameObject incorrectContextt;
    public Image bucketDropped;
    public bool isCorrect;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void SetOriginalPosition()
    {
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
        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = originalPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowDescription();
    }

    private void ShowDescription()
    {
        UIController.Instance.ToggleDescriptionPanel(true, 0);
        UIController.Instance.SetNameAndDescriptionText(animalNameText, descriptionText);
    }

    public void CheckCategoryMatch(CardCategory bucketCategory)
    {
        foreach (CardCategory cardCategory in categories)
        {
            if (cardCategory == bucketCategory)
            {
                Debug.Log("Card '" + animalNameText + "' matched category: " + bucketCategory.ToString());
                correctContext.SetActive(true);
                isCorrect = true;
                return;
            }
        }

        Debug.Log("Card '" + animalNameText + "' did not match category: " + bucketCategory.ToString());
        incorrectContextt.SetActive(true);
        isCorrect = false;
    }

    public void SetBucketDroppedSprite(Image image)
    {
        bucketDropped?.gameObject.SetActive(true);
        bucketDropped.sprite = image.sprite;
    }
}
