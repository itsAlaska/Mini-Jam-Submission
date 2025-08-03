using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField, TextArea] private string fullMessage;
    [SerializeField] private float typeDelay = 0.05f;
    [SerializeField] private AudioSource blipSound;

    private Coroutine typingCoroutine;

    public void StartTypewriter()
    {
        messageText.text = "";
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullMessage)
        {
            messageText.text += letter;

            if (blipSound != null && letter != ' ' && letter != '\n')
                blipSound.Play();

            yield return new WaitForSeconds(typeDelay);
        }
    }
}
