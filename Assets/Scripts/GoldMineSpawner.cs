using System.Collections;
using UnityEngine;

public class GoldMineSpawner : MonoBehaviour
{
    public GameObject goldBagPrefab;  // Assign the gold bag prefab in the inspector
    public Transform spawnPoint;      // Assign the spawn point in the inspector
    public float spawnInterval = 2f;  // Time between spawns
    public float moveDuration = 2f;   // Duration of the Lerp movement

    private void Start()
    {
        StartCoroutine(SpawnGoldBags());
    }

    private IEnumerator SpawnGoldBags()
    {
        while (true)
        {
            // Instantiate gold bag
            GameObject goldBag = Instantiate(goldBagPrefab, spawnPoint.position, Quaternion.identity);

            // Calculate a random target position within the grass area
            Vector3 targetPosition = GetRandomPositionInGrass();

            // Move the gold bag to the target position
            StartCoroutine(MoveGoldBag(goldBag, targetPosition, moveDuration));

            // Wait for the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator MoveGoldBag(GameObject goldBag, Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = goldBag.transform.position;

        while (time < duration)
        {
            goldBag.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        goldBag.transform.position = targetPosition;
    }

    private Vector3 GetRandomPositionInGrass()
    {
        // Define the boundaries of the grass area
        float minX = -4f; // Adjust based on your scene layout
        float maxX = 4f;
        float minY = -1f;
        float maxY = 1f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, randomY, 0);
    }
}