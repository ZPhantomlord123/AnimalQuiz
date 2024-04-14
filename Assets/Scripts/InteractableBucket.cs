using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableBucket : MonoBehaviour, IDropHandler
{
    public CardCategory category;
    public void OnDrop(PointerEventData eventData)
    {
        InteractableCard card = eventData.pointerDrag.GetComponent<InteractableCard>();

        if (card != null)
        {
            card.DisableCard();
            HandleDroppedCard(card);
        }
    }

    private void HandleDroppedCard(InteractableCard card)
    {
        // You can add additional logic here for handling the dropped card
        Debug.Log("Card dropped into the bucket: " + card.gameObject.name);
    }
}
