using UnityEngine;
using UnityEngine.UI;

public class GoblinController : MonoBehaviour
{
    public float speed = 2f;
    public Transform returnPoint;
    public Text goldCounterText;
    private bool isCarryingGold = false;
    private GameObject targetGoldBag = null;
    private int goldCount = 0;
    private Vector3 lastPosition;
    private Animator animator; // Animator component reference

    private Vector3 originalScale; // To store the original scale of the goblin

    private void Start()
    {
        lastPosition = transform.position;
        animator = GetComponent<Animator>(); // Get the Animator component
        originalScale = transform.localScale; // Save the goblin's original scale
    }

    private void Update()
    {
        if (!isCarryingGold)
        {
            FindNearestGoldBag();
            if (targetGoldBag != null)
            {
                MoveTowards(targetGoldBag.transform.position);

                if (Vector3.Distance(transform.position, targetGoldBag.transform.position) < 0.1f)
                {
                    PickUpGoldBag();
                }
            }
        }
        else
        {
            MoveTowards(returnPoint.position);

            if (Vector3.Distance(transform.position, returnPoint.position) < 0.1f)
            {
                ReturnGoldBag();
            }
        }

        // Handle animation by checking speed
        float velocity = (transform.position - lastPosition).magnitude;
        animator.SetFloat("Speed", velocity); // Assuming "Speed" parameter in Animator

        lastPosition = transform.position;
    }

    // Move towards the target position and flip the sprite accordingly
    private void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check movement direction and flip sprite
        if (target.x < transform.position.x) // Moving left
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Flip to face left
        }
        else if (target.x > transform.position.x) // Moving right
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Flip to face right
        }
    }

    // Find the nearest gold bag
    private void FindNearestGoldBag()
    {
        GameObject[] goldBags = GameObject.FindGameObjectsWithTag("GoldBag");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject goldBag in goldBags)
        {
            float distance = Vector3.Distance(transform.position, goldBag.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetGoldBag = goldBag;
            }
        }
    }

    // Handle picking up the gold bag
    private void PickUpGoldBag()
    {
        if (targetGoldBag != null)
        {
            isCarryingGold = true;
            targetGoldBag.SetActive(false); // Disable the gold bag (instead of destroying it)
        }
    }

    // Handle returning the gold bag
    private void ReturnGoldBag()
    {
        isCarryingGold = false;
        goldCount++;
        UpdateGoldCounter();
    }

    // Update the gold counter UI
    private void UpdateGoldCounter()
    {
        goldCounterText.text = ": " + goldCount;
    }

    // Public method to check if the goblin is carrying gold
    public bool IsCarryingGold()
    {
        return isCarryingGold;
    }

    // Public method to drop gold
    public void DropGold()
    {
        isCarryingGold = false; // Set carrying state to false
        // Add logic here to instantiate or show the dropped gold bag if needed
    }
}
