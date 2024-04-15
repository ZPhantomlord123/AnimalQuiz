using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject finishScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleDescriptionPanel(bool value, float delay)
    {
        StartCoroutine(ToggleUIWithDelay(descriptionPanel, value, delay));
    }
    
    public void SetNameAndDescriptionText(string name, string desc)
    {
        nameText.text = name;
        descriptionText.text = desc;
    }

    public void ToggleFinishScreen(bool value, float delay)
    {
        StartCoroutine(ToggleUIWithDelay(finishScreen, value, delay));
    }

    private IEnumerator ToggleUIWithDelay(GameObject uiElement, bool value, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (uiElement != null)
        {
            uiElement.SetActive(value);
        }
    }
}
