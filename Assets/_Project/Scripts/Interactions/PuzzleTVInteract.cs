using UnityEngine;

public class PuzzleTVInteract : Interactable
{
    [SerializeField] private Sprite[] imageOptions;
    [SerializeField] private SpriteRenderer screenRenderer;
    [SerializeField] private PuzzleTVManager manager;
    [SerializeField] private Sprite solvedImage;
    

    private int currentImageIndex = 0;

    public override void Interact()
    {
        base.Interact();

        currentImageIndex = (currentImageIndex + 1) % imageOptions.Length;
        screenRenderer.sprite = imageOptions[currentImageIndex];

        Debug.Log($"{gameObject.name} cycled to image {currentImageIndex}");
        
        manager.CheckPuzzleState();
    }

    public int GetCurrentImageIndex()
    {
        return currentImageIndex;
    }
    
    public Sprite GetCurrentSprite()
    {
        return screenRenderer.sprite;
    }
}