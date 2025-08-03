using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleTVManager : MonoBehaviour
{
    [SerializeField] private PuzzleTVInteract[] puzzleTVs;
    [SerializeField] private int[] correctImageIndices;
    [SerializeField] private GameObject memoryCutscene;
    [SerializeField] private Collider2D subwayStairsCollider;

    [Header("Events")]
    [SerializeField] private UnityEvent onPuzzleSolved;
    [SerializeField] private Transform hubReturnPoint;
    [SerializeField] private float cutsceneDuration = 4f; 
    [SerializeField] private TransitionManager screenFader; 
    [SerializeField] private Collider2D hubTVCollider;
    [SerializeField] private TVInteract hubTV;
    
    [SerializeField] private Transform playerMemorySpot;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 2f;

    private bool puzzleSolved = false;
    
    private void Start()
    {
        if (subwayStairsCollider != null)
            subwayStairsCollider.enabled = false;
    }


    public void CheckPuzzleState()
    {
        if (puzzleSolved) return;

        for (int i = 0; i < puzzleTVs.Length; i++)
        {
            if (puzzleTVs[i].GetCurrentImageIndex() != correctImageIndices[i])
                return;
        }

        puzzleSolved = true;
        OnPuzzleSolved();
    }

    private void OnPuzzleSolved()
    {
        Debug.Log("🎉 Puzzle Solved!");
        StartCoroutine(MovePlayerToSpot());
    }
    
    
    private IEnumerator MovePlayerToSpot()
    {
        if (player == null || playerMemorySpot == null)
            yield break;

        // Disable input
        PlayerMovement inputHandler = player.GetComponent<PlayerMovement>();
        if (inputHandler != null) inputHandler.enabled = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb == null) yield break;

        // Walk player to memory spot
        Vector2 target = playerMemorySpot.position;
        while (Vector2.Distance(rb.position, target) > 0.05f)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
            yield return null;
        }
        rb.MovePosition(target);

        // Show memory cutscene
        if (memoryCutscene != null)
        {
            memoryCutscene.SetActive(true);
        }

        yield return new WaitForSeconds(TransitionManager.Instance.fadeDuration);; // <- Add to inspector

        // Call the TransitionManager to fade + teleport
        if (TransitionManager.Instance != null && hubReturnPoint != null)
        {
            TransitionManager.Instance.TeleportPlayer(hubReturnPoint.position);
        }

        // Re-enable input after teleport (optional, could also delay via TransitionManager if needed)
        yield return new WaitForSeconds(TransitionManager.Instance.fadeDuration); // Wait for fade-in
        if (inputHandler != null) inputHandler.enabled = true;

        // Disable puzzle TVs
        foreach (var tv in puzzleTVs)
        {
            Collider2D col = tv.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }
        
        // Disable HUB TV
        if (hubTVCollider != null)
        {
            hubTVCollider.enabled = false;
        }
        
        // Move camera
        Camera.main.transform.position = new Vector3(0f, 0f, Camera.main.transform.position.z);
        
        // Set hub TV screen to solved image
        if (hubTV != null)
        {
            hubTV.SetSolvedImage(puzzleTVs[0].GetCurrentSprite());
        }
        
        // Enable subway stairs interaction
        if (subwayStairsCollider != null)
            subwayStairsCollider.enabled = true;

    }



}