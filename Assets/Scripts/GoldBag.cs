using UnityEngine;

public class GoldBag : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object it hit is tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Stop the gold bag by setting its velocity to zero
            rb.velocity = Vector2.zero;

            // Freeze its position to prevent further movement
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
