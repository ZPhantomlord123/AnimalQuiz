using UnityEngine;

public class PopupTransition : MonoBehaviour
{
    public float transitionDuration = 0.5f;
    public AnimationCurve scaleCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private Vector3 initialScale;
    private Vector3 targetScale;
    private bool isTransitioning = false;

    private void OnEnable()
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            initialScale = Vector3.zero;
            targetScale = Vector3.one;
            StartCoroutine(ScaleOverTime(initialScale, targetScale));
        }
    }

    private void OnDisable()
    {
        if (gameObject.activeInHierarchy) // Check if GameObject is active in hierarchy
        {
            if (!isTransitioning)
            {
                isTransitioning = true;
                initialScale = Vector3.one;
                targetScale = Vector3.zero;
                StartCoroutine(ScaleOverTime(initialScale, targetScale));
            }
        }
        else
        {
            transform.localScale = Vector3.zero; // Set scale immediately
        }
    }

    private System.Collections.IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, scaleCurve.Evaluate(elapsedTime / transitionDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
        isTransitioning = false;
    }
}
