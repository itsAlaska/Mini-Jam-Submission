using System;
using UnityEngine;

public class TVInteract : Interactable
{
     public override void Interact()
    {
        base.Interact();
        Debug.Log("Sizing up the stairs...");
        // Add specific behavior here
    }
}
