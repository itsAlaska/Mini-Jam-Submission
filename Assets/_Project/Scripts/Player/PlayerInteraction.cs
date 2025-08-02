using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    private Interactable currentInteractable;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    public void SetCurrentInteractable(Interactable interactable)
    {
        currentInteractable = interactable;
    }

    public void ClearCurrentInteractable(Interactable interactable)
    {
        if (currentInteractable == interactable)
        {
            currentInteractable = null;
        }
    }
}