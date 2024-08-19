using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject CameraHolder;
    public float moveSpeed = 5f;
    public float smoothTime = 0.3f; 

    private Vector3 velocity = Vector3.zero;
    private GameObject currentTargetObject;

    // Public function to assign the target object and start movement
    public void Move(GameObject TargetObject)
    {
        currentTargetObject = TargetObject;
        StartCoroutine(MoveTowardsTarget(currentTargetObject.transform.position));
    }

    private IEnumerator MoveTowardsTarget(Vector3 target)
    {
        while (Vector3.Distance(CameraHolder.transform.position, target) > 0.05f)
        {
            Vector3 direction = (target - CameraHolder.transform.position).normalized;
            Vector3 newPosition = Vector3.SmoothDamp(CameraHolder.transform.position, target, ref velocity, smoothTime, moveSpeed);

            // Update the camera position
            CameraHolder.transform.position = newPosition;

            yield return null; // Wait for the next frame
        }

        // Ensure the CameraHolder reaches the exact target position
        CameraHolder.transform.position = target;
    }


}
