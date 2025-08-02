using UnityEngine;

public class CDStackInteract : Interactable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Browsing the CD stack...");
        // Add specific behavior here
    }
}