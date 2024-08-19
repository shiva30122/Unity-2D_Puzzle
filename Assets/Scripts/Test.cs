using UnityEngine;
using UnityEngine.UI; 
using System.Collections; 

public class Test : MonoBehaviour
{
    public Text Password_Circle_red; 
    public DragAndDropManager manager; 
    public string Password; // Password to match
    public float Timer = 2f; // delay duration

    private void Start()
    {
        IsMatchPassword();
    }

    public void IsMatchPassword()
    {
        // Check if the Password text matches the Password
        if (Password_Circle_red != null && manager != null)
        {
            if (Password_Circle_red.text == Password)
            {
                //manager.placement = true; // Set placement to true
                Debug.Log("Password matched, placement enabled.");
            }
            else
            {
                Debug.Log("Password did not match.");
            }
        }
        else
        {
            Debug.LogWarning("Password_Circle_red or manager is not assigned.");
        }
    }

    // public void TriggerReplacementReset()
    // {
    //     StartCoroutine(SetToReplacement());
    // }

    // private IEnumerator SetToReplacement()
    // {
    //     yield return new WaitForSeconds(Timer);

    //     manager.placement = false; 

    //      // Reset position of the Drag Object
    //     manager.draggableObject.transform.position = manager.originalPosition.transform.position;

    //     Debug.Log("Placement reset To True .  "+manager.placement);


    //     yield return new WaitForSeconds(2); 
    //     manager.placement = true;
    //     Debug.Log("Placement reset To True .  "+manager.placement);


    // }

    public AudioSource Audio; 
    public AudioClip unlockSound;   

    public void UnlockedPassword()
    {
        NotifyTextManager.UpdateNotifyText("  YOU UNLOCKED !..... ",3);
        Debug.Log("   UNLOCKED  !!!...... ");

        Audio.Stop();
        Audio.PlayOneShot(unlockSound);

    }

    public void MessageToSolvePuzzle()
    {
        NotifyTextManager.UpdateNotifyText("  Solve The PUzzle !..... ",3);
    }



}
