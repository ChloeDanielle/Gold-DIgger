using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;

    public void Initialize(Vector3 shootDirection)
    {
        direction = shootDirection;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        
        // Destroy the arrow after it goes off screen
        if (transform.position.y > Camera.main.orthographicSize || 
            transform.position.y < -Camera.main.orthographicSize || 
            transform.position.x < -Camera.main.orthographicSize * Camera.main.aspect || 
            transform.position.x > Camera.main.orthographicSize * Camera.main.aspect)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goblin"))
        {
            // Call the methods in GoblinController to drop gold
            GoblinController goblinController = other.GetComponent<GoblinController>();
            if (goblinController != null)
            {
                goblinController.DropGold(); // Make sure this method exists
            }

            Destroy(gameObject); // Destroy the arrow on hit
        }
    }
}
