using UnityEngine;
using UnityEngine.Events;

public class DragAndDropManager : MonoBehaviour
{
    public GameObject draggableObject;            // Reference to the draggable OBJECT
    public GameObject targetObject;               // Reference to the target OBJECT
    public GameObject originalPositionObject;     // GameObject defining the original position

    public float targetDistance = 0.5f;           // Maximum allowable distance for PLACEMENT
    public float dragSpeed = 10f;                 // Speed of dragging

    public string dragMessage = "Object is dragged";
    public string dropMessageCorrect = "Object is placed correctly!";
    public string dropMessageIncorrect = "Try again!";
    public string onClickTarget = "Object clicked!"; // Message for the first-time click
    public string onTargetClickMessage = "Target clicked!"; // Message for clicking the target

    public UnityEvent onCorrectPlacement;         // Event triggered on correct PLACEMENT
    public UnityEvent onIncorrectPlacement;       // Event triggered on incorrect PLACEMENT
    public UnityEvent onFirstClick;                // Event triggered on the first click
    public UnityEvent onTargetClick;               // Event triggered on target click

    public AudioSource audioSource;
    public AudioSource Music;
    public AudioClip dragSound;                   // Sound played when dragging starts
    public AudioClip dropSound;                   // Sound played when dropping
    public AudioClip placedSound;                 // Sound played when correctly placed

    private bool isDragging = false;              // Track whether the object is currently being dragged
    private Vector3 originalPosition;             // Position of the draggableObject
    private bool isFirstClick = true;             // Track if the object has been clicked for the first time

    public bool placement = false;                // Control whether placement is allowed

    public AudioClip onCorrectPlacementMusic;

    void Start()
    {
        if (originalPositionObject != null)
        {
            originalPosition = originalPositionObject.transform.position;
        }
        else
        {
            Debug.LogWarning("Original Position GameObject is not assigned.");
        }
    }

    void Update()
    {

        if(placement)return;



        if (draggableObject == null || targetObject == null)
        {
            Debug.LogError("Draggable Object or Target Object is not assigned.");
            return;
        }

        // Only process if placement is false
        if (!placement)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure z-coordinate is zero for 2D

            if (Input.GetMouseButtonDown(0))
            {
                if (Vector2.Distance(mousePosition, draggableObject.transform.position) < targetDistance)
                {
                    if (isFirstClick)
                    {
                        ResetToOriginalPosition();
                        NotifyTextManager.UpdateNotifyText(onClickTarget, 4); // Display OnClickTarget message
                        onFirstClick?.Invoke(); // Trigger event on the first click
                        isFirstClick = false;
                        return; // Exit the update loop to prevent further processing
                    }

                    isDragging = true; // Start dragging
                    PlayAudio(dragSound); // Play drag sound
                    NotifyTextManager.UpdateNotifyText(dragMessage, 4); // Display drag message
                }
                else if (Vector2.Distance(mousePosition, targetObject.transform.position) < targetDistance)
                {
                    // Target clicked
                    NotifyTextManager.UpdateNotifyText(onTargetClickMessage, 4); // Display target click message
                    onTargetClick?.Invoke(); // Trigger event on target click
                }
            }

            if (isDragging && Input.GetMouseButton(0))
            {
                // Smoothly move the object towards the mouse position
                Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, draggableObject.transform.position.z);
                draggableObject.transform.position = Vector3.Lerp(draggableObject.transform.position, targetPosition, Time.deltaTime * dragSpeed);
                Debug.Log("Dragging object to position: " + draggableObject.transform.position);
            }

            if (isDragging && Input.GetMouseButtonUp(0))
            {
                isDragging = false; // Stop dragging
                Debug.Log("Dragging stopped.");
                CheckPlacement();
            }
        }
    }

    private void CheckPlacement()
    {
        if (targetObject != null)
        {
            // Check if the draggable object is within the target's allowable distance
            float distance = Vector3.Distance(draggableObject.transform.position, targetObject.transform.position);
            Debug.Log("Distance to target: " + distance);

            if (distance <= targetDistance)
            {
                draggableObject.transform.position = targetObject.transform.position;
                PlayAudio(placedSound); // Play placed sound
                NotifyTextManager.UpdateNotifyText(dropMessageCorrect, 3); // Display correct drop message
                onCorrectPlacement?.Invoke(); // Trigger correct placement event
                Music.Stop();
                Music.loop = true;
                Music.clip = onCorrectPlacementMusic;
                Music.Play();
                placement = true;
            }
            else
            {
                draggableObject.transform.position = originalPosition;
                PlayAudio(dropSound); // Play drop sound
                NotifyTextManager.UpdateNotifyText(dropMessageIncorrect, 3); // Display incorrect drop message
                onIncorrectPlacement?.Invoke(); // Trigger incorrect placement event
            }
        }
    }

    private void ResetToOriginalPosition()
    {
        if (draggableObject != null)
        {
            draggableObject.transform.position = originalPosition;
            Debug.Log("Object reset to original position: " + originalPosition);
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Stop any currently playing audio
            }
            audioSource.PlayOneShot(clip);
        }
    }
}
