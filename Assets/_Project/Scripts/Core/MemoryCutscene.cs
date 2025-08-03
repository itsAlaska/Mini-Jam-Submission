using UnityEngine;

public class MemoryCutscene : MonoBehaviour
{
    [SerializeField] private SpriteRenderer friendSprite;
    [SerializeField] private float fadeDuration = 1f;

    private void OnEnable()
    {
        if (friendSprite != null)
            StartCoroutine(FadeIn());
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float timer = 0f;
        Color color = friendSprite.color;
        color.a = 0f;
        friendSprite.color = color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            friendSprite.color = color;
            yield return null;
        }
    }
}