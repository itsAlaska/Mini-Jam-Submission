using System.Collections;
using UnityEngine;

public class StairwayInteract : Interactable
{
    [SerializeField] private CanvasGroup endingCanvasGroup;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private string finalMessage = "You made it underground.\nBut they were never really lost.";

    public override void Interact()
    {
        HidePrompt();
        base.Interact();
        Debug.Log("Player is entering the stairwell...");
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        if (endingCanvasGroup == null)
        {
            Debug.LogWarning("No endingCanvasGroup assigned.");
            yield break;
        }

        // Fade to black
        float timer = 0f;
        endingCanvasGroup.blocksRaycasts = true;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            endingCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        endingCanvasGroup.alpha = 1f;

        // Wait a moment
        yield return new WaitForSeconds(1f);

        // Start the typewriter effect
        TypewriterEffect typer = endingCanvasGroup.GetComponentInChildren<TypewriterEffect>();
        if (typer != null)
        {
            typer.StartTypewriter();
        }
        else
        {
            Debug.LogWarning("No TypewriterEffect found on endingCanvasGroup.");
        }
    }

}