using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] public Transform worldTargetPosition;
    [SerializeField] public Transform cameraTargetPosition;
    [SerializeField] public Transform cameraTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPrompt();
            PlayerInteraction.Instance.SetCurrentInteractable(this); // more on this below
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HidePrompt();
            PlayerInteraction.Instance.ClearCurrentInteractable(this);
        }
    }

    public virtual void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}");
        // Add logic here or override
    }

    public void ShowPrompt()
    {
        if (promptUI != null)
            promptUI.SetActive(true);
    }

    public void HidePrompt()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }
}