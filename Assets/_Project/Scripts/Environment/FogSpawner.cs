using UnityEngine;

public class FogSpawner : MonoBehaviour
{
    [Header("Fog Settings")]
    public GameObject fogPrefab;
    public int maxFogCount = 5;
    public float spawnInterval = 2f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    [Header("Despawn Settings")]
    public float despawnX = -20f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && CountFogObjects() < maxFogCount)
        {
            int currentCount = CountFogObjects();
            
            if (currentCount < maxFogCount)
            {
                SpawnFog();
                timer = 0f;
            }
        }

        CleanupFog();
    }

    void SpawnFog()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject fog = Instantiate(fogPrefab, spawnPos, Quaternion.identity, transform);
        
        float scaleY = Random.Range(0.3f, 1f);
        float scaleX = Random.value > .5f ? 1 : -1;
        if (Random.value > 0.5f)
        {
            scaleY *= -1f;
        }
        
        fog.transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    void CleanupFog()
    {
        foreach (Transform child in transform)
        {
            if (child.position.x > despawnX)
            {
                Destroy(child.gameObject);
            }
        }
    }

    int CountFogObjects()
    {
        return transform.childCount;
    }
}