using UnityEngine;

public class HideUiClick : MonoBehaviour
{
    // Reference to the SpriteRenderer component
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component on this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;
        
        // Check if the SpriteRenderer component is present
        if (spriteRenderer == null)
        {
            Debug.LogWarning("No SpriteRenderer found on this GameObject.");
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // Perform a 2D raycast from the mouse position
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // Check if the ray hits this GameObject
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Hide the sprite by disabling the SpriteRenderer
                HideSprite();
            }
        }
    }

    void HideSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
            Debug.Log("Sprite hidden.");
        }
    }
}
