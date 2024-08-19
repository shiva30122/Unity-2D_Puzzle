using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PasswordController : MonoBehaviour
{
    public List<PasswordEntryManager> passwordEntries; // List of PasswordEntryManager Instances
    public string Password; // Correct password to match
    public UnityEvent onPasswordMatch; // Event triggered when the PASWORD Matches !...
    public UnityEvent NotPasswordMatch;// Event triggered when the PASWORD Matches !...

    public Collider2D checkCollider; // Collider to check if PRESED CLICK !.. 

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (checkCollider != null && checkCollider.OverlapPoint(mousePosition))
            {
                CheckPassword();
            }
        }
    }


    public AudioSource Audio; 
    public AudioClip ClickSound;

    private void CheckPassword()

    {

        Audio.PlayOneShot(ClickSound);

        string combinedString = GetCombinedPasswordString();

        Debug.Log($"Combined password: {combinedString}");

        if (combinedString == Password)
        {
            Debug.Log("Password matched!");
            onPasswordMatch.Invoke();
        }
        else if (combinedString != Password)
        {
            Debug.Log("Password did not match.");
            NotPasswordMatch.Invoke();
        }
    }

    private string GetCombinedPasswordString()
    {
        string result = "";

        foreach (PasswordEntryManager manager in passwordEntries)
        {
            if (manager != null && manager.Strings.Count > 0)
            {
                result += manager.Strings[manager.currentIndex]; // Append the current string from each manager
            }
        }

        return result;
    }
}
