using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class QuizManager : MonoBehaviour
{
    public List<InteractableCard> cards;
    public InteractableBucket redBucket;
    public InteractableBucket blueBucket;
    public TextMeshProUGUI redBucketText;
    public TextMeshProUGUI blueBucketText;
    public TextMeshProUGUI correctScoreText;
    private int correctScoreCount = 0;
    public TextMeshProUGUI incorrectScoreText;
    private int incorrectScoreCount = 0;
    public TextMeshProUGUI finalScoreText;

    public int cardCount = 0;

    private void Start()
    {
        RandomizeCardPositions();
        SetRandomCategory();
        UpdateBucketText();

        // Subscribe to the OnCardDropped event of redBucket and blueBucket
        redBucket.OnCardDropped += HandleCardDropped;
        blueBucket.OnCardDropped += HandleCardDropped;
    }

    private void RandomizeCardPositions()
    {
        ShuffleCardPositions();
        // Store the initial positions after shuffling
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetOriginalPosition();
        }
    }

    private void ShuffleCardPositions()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            Vector3 tempPosition = cards[i].rectTransform.anchoredPosition;
            cards[i].rectTransform.anchoredPosition = cards[randomIndex].rectTransform.anchoredPosition;
            cards[randomIndex].rectTransform.anchoredPosition = tempPosition;
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

    public void OnDescriptionCloseButtonPressed()
    {
        UIController.Instance.ToggleDescriptionPanel(false, 0);
    }

    private void HandleCardDropped()
    {
        cardCount++;
        if (cardCount >= cards.Count)
        {
            DisplayFinalResult();
        }
    }

    public void DisplayFinalResult()
    {
        Debug.Log("All cards have been dropped!");
        UIController.Instance.ToggleFinishScreen(true, 0);
        // Snap cards back to shuffled positions & update score
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].gameObject.SetActive(true);
            if (cards[i].isCorrect)
            {
                correctScoreCount++;
            }
            else
            {
                incorrectScoreCount++;
            }
        }
        float finalScore = ((float)correctScoreCount / (float)cards.Count) * 100f;
        finalScoreText.text = "Your Score : " + finalScore.ToString("F0") + "%";
        correctScoreText.text = "- " + correctScoreCount.ToString();
        incorrectScoreText.text = "- " + incorrectScoreCount.ToString();

    }
    public void OnPlayAgainButtonClicked()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
