using UnityEngine;

public class ArcherController : MonoBehaviour
{
    public float shootingRange = 5f; // Distance within which the archer can detect the goblin
    public GameObject arrowPrefab; // Reference to the arrow prefab
    public Transform arrowSpawnPoint; // Point where the arrow will spawn
    public float shootingCooldown = 1f; // Time between shots
    private float lastShotTime = 0f;

   private void Update()
{
    GameObject goblin = FindClosestGoblin();
    
    if (goblin != null)
    {
        // Check if the goblin is within range
        float distanceToGoblin = Vector3.Distance(transform.position, goblin.transform.position);
        if (distanceToGoblin <= shootingRange && Time.time >= lastShotTime + shootingCooldown)
        {
            GetComponent<Animator>().SetTrigger("Shoot"); // Trigger the shooting animation
            ShootArrow(goblin.transform.position);
            lastShotTime = Time.time; // Reset the shot timer
        }
    }
}

    private GameObject FindClosestGoblin()
    {
        GameObject[] goblins = GameObject.FindGameObjectsWithTag("Goblin");
        GameObject closestGoblin = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject goblin in goblins)
        {
            float distance = Vector3.Distance(transform.position, goblin.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGoblin = goblin;
            }
        }

        return closestGoblin;
    }

    private void ShootArrow(Vector3 targetPosition)
    {
        // Create the arrow and set its direction
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        Vector3 shootDirection = (targetPosition - arrowSpawnPoint.position).normalized;
        arrow.GetComponent<ArrowController>().Initialize(shootDirection);
    }
}
