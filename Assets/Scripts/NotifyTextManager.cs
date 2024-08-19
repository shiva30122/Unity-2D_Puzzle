using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotifyTextManager : MonoBehaviour
{
    public static NotifyTextManager Instance { get; private set; } // Singleton instance

    public Text notifyText; 
    public float messageDuration = 2.0f; // Duration before the MESSAGE is Cleared 

    private Coroutine clearTextCoroutine;

    private void Awake()
    {
        // Ensure only one instance of NotifyTextManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of NotifyTextManager detected! Destroying extra instance.");
            Destroy(gameObject);
            return;
        }
        
        if (notifyText == null)
        {
            Debug.LogError("NotifyText component is not assigned in the Inspector.");
        }
    }


    public static void UpdateNotifyText(string message , float SetTime)// TODO  add BOOL to not clear wntill next objects or anny thing happens!....
    {
        if (Instance != null && Instance.notifyText != null)
        {

            Instance.messageDuration = SetTime; // add for communcating scripts which has more time to read !...
            
            Instance.notifyText.text = message;
            Debug.Log("NotifyText updated: " + message); 

            if (Instance.clearTextCoroutine != null)
            {
                Instance.StopCoroutine(Instance.clearTextCoroutine);
            }
            Instance.clearTextCoroutine = Instance.StartCoroutine(Instance.ClearTextAfterDelay());
        }
        else
        {
            if (Instance == null)
            {
                Debug.LogWarning("NotifyTextManager Instance is null.");
            }
           
        }
    }
    public static void UpdateNotifyButton(string message ) // cant add FLOAT and STRING !....
    
    {
        if (Instance != null && Instance.notifyText != null)
        {
            Instance.notifyText.text = message;
            Debug.Log("NotifyText updated: " + message); 

            if (Instance.clearTextCoroutine != null)
            {
                Instance.StopCoroutine(Instance.clearTextCoroutine);
            }
            Instance.clearTextCoroutine = Instance.StartCoroutine(Instance.ClearTextAfterDelay());
        }
        else
        {
            if (Instance == null)
            {
                Debug.LogWarning("NotifyTextManager Instance is null.");
            }
           
        }
    }


    private IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        if (notifyText != null)
        {
            notifyText.text = "";
            Debug.Log("NotifyText cleared."); 
        }
    }
}
