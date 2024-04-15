using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableBucket : MonoBehaviour, IDropHandler
{
    public CardCategory category;

    public event Action OnCardDropped;
    public void OnDrop(PointerEventData eventData)
    {
        InteractableCard card = eventData.pointerDrag.GetComponent<InteractableCard>();

        if (card != null)
        {
            card.SnapBack(2f);
            card.DisableCard();
            HandleDroppedCard(card);
        }
    }

    private void HandleDroppedCard(InteractableCard card)
    {
        card.SetBucketDroppedSprite(GetComponent<Image>());
        card.CheckCategoryMatch(category);
        OnCardDropped?.Invoke();
    }
}
