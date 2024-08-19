using UnityEngine;
using System.Collections;

public class SwapMoveMent : MonoBehaviour
{

    public GameObject ObjectMove; // The GameObject to move
    public Transform CenterPoint; // The center of the circle
    public float radius = 5f; // Radius of the circle
    public float speed = 2f; // Speed of rotation

    public Collider2D targetCollider; // Collider of the target object
    public bool moveForward = true; // Flag to determine direction of rotation (true = clockwise, false = counterclockwise)
    public bool isMoving = false; // Flag to start moving

    private float angle;
    private Vector2 targetPosition2D; // Target position based on target collider

    void Start()
    {
        if (CenterPoint == null || targetCollider == null || ObjectMove == null)
        {
            Debug.LogError("Center, Target Collider, or ObjectMove not assigned. ERROR 404");
            return;
        }

        // Initialize the target position
        ResetMovement();
    }

    // AREADY MADED BUT SOME ADDED CHANGES MADE BY -
    //SHAR-MI DEV_(SIVA_BALL)_ME AND + CHAT_GPT !<<<.... FOR ROTATION MATHT.PI !<<<<....

    public void StartMovement(Transform TargetPoint, float Radius)
    {
        // Update target position dynamically
        if (TargetPoint != null)
        {
            CenterPoint = TargetPoint;
        }

        radius = Radius;

        // Stop the current movement and reset for the new target
        StopCoroutine(CircularMovementCoroutine());
        ResetMovement();
        isMoving = true;
        StartCoroutine(CircularMovementCoroutine());
    }

    private void ResetMovement()
    {
        Vector2 currentPosition = ObjectMove.transform.position;
        angle = Mathf.Atan2(currentPosition.y - CenterPoint.position.y, currentPosition.x - CenterPoint.position.x);
        targetPosition2D = (Vector2)targetCollider.transform.position;
    }

    private IEnumerator CircularMovementCoroutine()
    {
        while (isMoving)
        {
            // Determine the direction of rotation
            if (moveForward)
            {
                angle += speed * Time.deltaTime; // Clockwise
            }
            else
            {
                angle -= speed * Time.deltaTime; // Counterclockwise
            }

            angle = Mathf.Repeat(angle, 2 * Mathf.PI); // Normalize angle to [0, 2 * PI]

            // Calculate the new position
            float x = CenterPoint.position.x + Mathf.Cos(angle) * radius;
            float y = CenterPoint.position.y + Mathf.Sin(angle) * radius;

            ObjectMove.transform.position = new Vector2(x, y);

            // Dynamically update target position
            targetPosition2D = (Vector2)targetCollider.transform.position;

            // Check if the object is within a small distance of the target collider
            if (Vector2.Distance(ObjectMove.transform.position, targetPosition2D) < radius * 0.2f) // Adjust this threshold as needed
            {
                // Stop movement and start attraction to the target
                isMoving = false;
                StartCoroutine(AttractToCenter());
                yield break; // Exit the coroutine
            }

            yield return null; // Wait until the next frame
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == targetCollider)
        {
            isMoving = false;
            StartCoroutine(AttractToCenter());
        }
        // SwapHolder obj = other.GetComponent<SwapHolder>();
        // if (obj)
        // {
        //     obj.IsMatchingObjects();
        //     print(" COLLIDED OBJ ISMATCHING !>>>  ");
        // }
    }

    private IEnumerator AttractToCenter()
    {
        Vector2 targetPosition = targetCollider.transform.position;

        while (Vector2.Distance(ObjectMove.transform.position, targetPosition) > 0.01f)
        {
            // Move ObjectMove toward the center of targetCollider smoothly
            ObjectMove.transform.position = Vector2.MoveTowards(
                ObjectMove.transform.position,
                targetPosition,
                speed * Time.deltaTime
            );

            yield return null; // Wait until the next frame
        }

        // Snap to the exact center after getting close enough
        ObjectMove.transform.position = targetPosition;

    }
}
