using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private CanvasGroup fadeCanvas;
    public float fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void TeleportPlayer(Vector3 targetPosition)
    {
        StartCoroutine(FadeAndTeleport(targetPosition));
    }
    
    private void PlayCRTShutoffEffect()
    {
        // TODO: Play sound / effect / shader
    }

    private void PlayCRTStartupEffect()
    {
        // TODO: Play sound / effect / shader
    }

    private IEnumerator FadeAndTeleport(Vector3 targetPos)
    {
        PlayCRTShutoffEffect();  // Placeholder for TV power down

        yield return Fade(1f); // Fade to black
        player.position = targetPos;

        PlayCRTStartupEffect();  // Placeholder for TV power up
        yield return Fade(0f); // Fade back in
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeCanvas == null) yield break;

        fadeCanvas.blocksRaycasts = true;
        float startAlpha = fadeCanvas.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = targetAlpha;
        fadeCanvas.blocksRaycasts = (targetAlpha > 0);
    }
}