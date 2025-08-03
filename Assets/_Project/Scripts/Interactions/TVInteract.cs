using System;
using UnityEngine;

public class TVInteract : Interactable
{
    [SerializeField] private SpriteRenderer screenRenderer;
    [SerializeField] private Sprite solvedImage;
    
     public override void Interact()
    {
        base.Interact();
        if (worldTargetPosition != null)
        {
            TransitionManager.Instance.TeleportPlayer(worldTargetPosition.position);
        }
        if (cameraTargetPosition != null)
        {
            cameraTarget.position = new Vector3(
                cameraTargetPosition.position.x,
                cameraTargetPosition.position.y,
                cameraTarget.position.z
            );
        }
        
        
    
        
    }
     public void SetSolvedImage(Sprite newImage)
     {
         solvedImage = newImage;
         screenRenderer.sprite = solvedImage;
     }
}
