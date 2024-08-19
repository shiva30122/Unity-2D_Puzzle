using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PasswordEntryManager : MonoBehaviour
{
    public Text displayText; 
    public List<string> Strings; // List of possible strings
    public int currentIndex = 0; // Current index in the String array !...
    public BoxCollider2D boxCollider;

    private void Start()
    {
        int maxRepetitions = Strings.Count; // Max NoOf Repat

        if (Strings.Count > 0)
        {
            displayText.text = Strings[currentIndex];
        }
        else
        {
            Debug.LogWarning("Strings list is empty.");
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (boxCollider != null && boxCollider.OverlapPoint(mousePosition))
            {
                Debug.Log("BoxCollider clicked.");
                CycleNextPasswordOption();
            }
            else
            {
                Debug.Log("Mouse click did not hit the BoxCollider.");
            }
        }
    }

    private void CycleNextPasswordOption()
    {
        if (Strings.Count == 0) return; 

        // Increment the current index to Move On Next INDEX !...
        
        currentIndex = (currentIndex + 1) % Strings.Count;
        displayText.text = Strings[currentIndex];
        Debug.Log($"Password option changed to: {displayText.text}");
    }
}
