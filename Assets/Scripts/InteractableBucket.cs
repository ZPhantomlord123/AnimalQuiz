using UnityEngine;

public class InteractableBucket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableCard card = collision.GetComponent<InteractableCard>();
        if (card != null)
        {
            HandleDroppedCard(card);
        }
    }

    private void HandleDroppedCard(InteractableCard card)
    {
        // Place logic here for handling the dropped card
        Debug.Log("Card dropped into Bucket: " + gameObject.name);
    }
}
