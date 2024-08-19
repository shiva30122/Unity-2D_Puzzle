using UnityEngine;
using System.Collections;

public class SwapController : MonoBehaviour
{

//FIRST SCRIPT FOR MOVEMENT THE IT CHAGED TO SWAPMOVEMENT BECAUES IT IS NOT WORKING WITH
// MY IDEA !... AND LITTLE COMPLEX TO SLOVE LOGIC AND CHAT_GPT !...


    public GameObject ImageChild; // Reference to the child GameObject

    public Collider2D TravelColliderHalf; // Collider defining the half travel path
    public Collider2D TravelColliderFull; // Collider defining the full travel path
    public Collider2D TravelColliderCross; // Collider for cross path

    public GameObject TargetGameObject; // GameObject defining the target position
    public float MoveSpeed = 2.8f; // Speed of the movement
    public float StopThreshold = 0.1f; // Distance threshold to consider as "reached" the target

    private float currentAngle = 0f; // Current angle in degrees
    private Vector2 pathCenter; // Center of the path
    private float radius; // Radius of the circle or approximate radius of the polygon
    private Vector2 targetPosition; // Target position on the edge
    private Coroutine movementCoroutine; // Reference to the current coroutine

    private Collider2D selectedCollider; // Reference to the selected Collider2D

    void Start()
    {
        // Print details of all colliders
        PrintColliderDetails(TravelColliderFull);
        PrintColliderDetails(TravelColliderHalf);
        PrintColliderDetails(TravelColliderCross);
    }

    private void PrintColliderDetails(Collider2D collider)
    {
        if (collider != null)
        {
            string colliderType = collider is CircleCollider2D ? "CircleCollider2D" :
                                  collider is PolygonCollider2D ? "PolygonCollider2D" :
                                  "UnknownColliderType";

            Debug.Log($"Collider Name: {collider.name}, Type: {colliderType}");
        }
        else
        {
            Debug.Log("Collider is null.");
        }
    }

    public void FullPathCollider(Collider2D collider)
    {
        selectedCollider = collider;
        if (selectedCollider != null)
        {
            if (selectedCollider is PolygonCollider2D polygonCollider)
            {
                pathCenter = polygonCollider.bounds.center;
                radius = Mathf.Max(polygonCollider.bounds.size.x, polygonCollider.bounds.size.y) / 2;
            }
            MoveObject(); // Move the object after selecting the full path
        }
        else
        {
            Debug.LogError("Selected collider is null.");
        }
    }

    public void HalfPathCollider(Collider2D collider)
    {
        selectedCollider = collider;
        if (selectedCollider != null)
        {
            if (selectedCollider is CircleCollider2D circleCollider)
            {
                pathCenter = circleCollider.transform.position;
                radius = circleCollider.radius * Mathf.Max(circleCollider.transform.lossyScale.x, circleCollider.transform.lossyScale.y);
            }
            MoveObject(); // Move the object after selecting the half path
        }
        else
        {
            Debug.LogError("Selected collider is null.");
        }
    }

    public void CoressPathCollider(Collider2D collider)
    {
        selectedCollider = collider;
        if (selectedCollider != null)
        {
            if (selectedCollider is CircleCollider2D circleCollider)
            {
                pathCenter = circleCollider.transform.position;
                radius = circleCollider.radius * Mathf.Max(circleCollider.transform.lossyScale.x, circleCollider.transform.lossyScale.y);
            }
            MoveObject(); // Move the object after selecting the cross path
        }
        else
        {
            Debug.LogError("Selected collider is null.");
        }
    }

    private void MoveObject()
    {
        if (selectedCollider != null)
        {
            // Recalculate the target position
            targetPosition = (Vector2)TargetGameObject.transform.position;

            // Stop any currently running coroutine
            if (movementCoroutine != null)
            {
                StopCoroutine(movementCoroutine);
            }

            // Start the movement coroutine
            movementCoroutine = StartCoroutine(MoveAlongPathCoroutine());
        }
        else
        {
            Debug.LogError("No collider selected.");
        }
    }

    private IEnumerator MoveAlongPathCoroutine()
    {
        // Calculate the initial position on the path
        Vector2 initialPosition = ImageChild.transform.position;

        if (selectedCollider is CircleCollider2D)
        {
            currentAngle = Mathf.Atan2(initialPosition.y - pathCenter.y, initialPosition.x - pathCenter.x) * Mathf.Rad2Deg;
        }
        else if (selectedCollider is PolygonCollider2D)
        {
            currentAngle = 0f; // Start angle for polygons; this may need adjustment based on your needs
        }

        while (true)
        {
            Vector2 pathPosition = Vector2.zero;

            if (selectedCollider is CircleCollider2D)
            {
                // Calculate position on the circular path
                pathPosition = new Vector2(
                    pathCenter.x + radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                    pathCenter.y + radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad)
                );
            }
            else if (selectedCollider is PolygonCollider2D)
            {
                // Handle polygon path movement here
                // This is a placeholder and should be replaced with actual polygon path logic
                pathPosition = new Vector2(
                    pathCenter.x + radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                    pathCenter.y + radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad)
                );
            }

            // Set the position of the ImageChild GameObject on the path edge
            ImageChild.transform.position = pathPosition;

            // Check if the current position is close enough to the target
            if (Vector2.Distance(ImageChild.transform.position, targetPosition) <= StopThreshold)
            {
                Debug.Log("Target reached!");

                // Ensure ImageChild is exactly at the target position
                ImageChild.transform.position = TargetGameObject.transform.position;
                ImageChild.transform.localPosition = Vector3.zero; // Reset local position to ensure it's aligned perfectly with the target

                // Exit the coroutine
                yield break;
            }

            // Move the angle along the path
            currentAngle += MoveSpeed * Time.deltaTime;

            // Keep the angle within 0-360 degrees
            if (currentAngle >= 360f)
            {
                currentAngle -= 360f;
            }

            // Wait for the next frame
            yield return null;
        }
    }
}
