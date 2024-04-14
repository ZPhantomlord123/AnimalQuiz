using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<InteractableCard> cards;
    public InteractableBucket redBucket;
    public InteractableBucket blueBucket;
    public TextMeshProUGUI redBucketText;
    public TextMeshProUGUI blueBucketText;

    private CardCategory currentCategory;
    private List<Vector3> initialCardPositions = new List<Vector3>();
    private List<string> correctMatches = new List<string>();
    private List<string> incorrectMatches = new List<string>();

    private void Start()
    {
        RandomizeCardPositions();
        SetRandomCategory();
        UpdateBucketText();
    }

    private void RandomizeCardPositions()
    {
        foreach (InteractableCard card in cards)
        {
            initialCardPositions.Add(card.transform.position);
        }

        ShuffleCardPositions();
    }

    private void ShuffleCardPositions()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            Vector3 tempPosition = cards[i].transform.position;
            cards[i].transform.position = cards[randomIndex].transform.position;
            cards[randomIndex].transform.position = tempPosition;
        }
    }

    private void SetRandomCategory()
    {
        int categoryCount = System.Enum.GetValues(typeof(CardCategory)).Length;

        // Generate a random index for the categories (0, 1, 2, ...)
        int randomIndex = Random.Range(0, categoryCount / 2) * 2;

        // Get the corresponding category pair for the random index
        CardCategory category1 = (CardCategory)randomIndex;
        CardCategory category2 = (CardCategory)(randomIndex + 1);

        // Assign the categories to the buckets
        redBucket.category = category1;
        blueBucket.category = category2;

        Debug.Log("Red Bucket Category: " + redBucket.category.ToString());
        Debug.Log("Blue Bucket Category: " + blueBucket.category.ToString());
    }



    private void UpdateBucketText()
    {
        redBucketText.text = redBucket.category.ToString();
        blueBucketText.text = blueBucket.category.ToString();
    }

    private CardCategory GetRandomOtherCategory(CardCategory category)
    {
        CardCategory[] categories = (CardCategory[])System.Enum.GetValues(typeof(CardCategory));
        CardCategory randomCategory = category;

        while (randomCategory == category)
        {
            randomCategory = categories[Random.Range(0, categories.Length)];
        }

        return randomCategory;
    }

    // Methods for accessing match data for later use
    public List<string> GetCorrectMatches()
    {
        return correctMatches;
    }

    public List<string> GetIncorrectMatches()
    {
        return incorrectMatches;
    }
}
