using UnityEngine;

public class StairwayInteract : Interactable
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Checking out the TV...");
        // Add specific behavior here
    }
}
