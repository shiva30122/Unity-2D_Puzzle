using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class OnClickTouch : MonoBehaviour
{

    public string NotifyMessageClick;
    public UnityEvent OnClick;
    //public UnityEvent CantClick;

    public AudioClip ClickSound; // Sound to play on successful click
    public AudioClip CantClickSound; // Sound to play if cooldown is active
    public AudioSource AudioPlayer; // AudioSource component to play sounds

    public float DelayTime = 1f; // Cooldown time in seconds

    private float _lastClickTime;

    // List to hold all OnClickTouch instances (non-static, unique to each script instance)
    public List<OnClickTouch> allOnClickTouchInstances = new List<OnClickTouch>();

    public SwapHolder CheckManager; // Reference to the SwapHolder script



    private void Start()
    {
        // Find all instances of OnClickTouch and add them to the list
        OnClickTouch[] allInstances = FindObjectsOfType<OnClickTouch>();

        foreach (OnClickTouch instance in allInstances)
        {
            allOnClickTouchInstances.Add(instance);
        }
    }

    private void OnMouseDown()
    {


        if( CanClikButton == false)
        {
            PlaySound(CantClickSound);


            if (NotifyMessageClick == null) return; 
            {
                NotifyTextManager.UpdateNotifyText(NotifyMessageClick,4);   

                return;
            }

        }
        // If this object is on cooldown, play CantClickSound and return
        if (Time.time - _lastClickTime < DelayTime)
        {
            PlaySound(CantClickSound);
            return;
        }

        // Update the time of the last click for this object
        _lastClickTime = Time.time;

        // Invoke the UnityEvent
        OnClick?.Invoke();

        // Play ClickSound on successful click
        PlaySound(ClickSound);

        // Set the cooldown for all other objects in the list
        SetCooldownForAll();

        // Start coroutine to check manager after delay
        StartCoroutine(CallCheckManagerAfterDelay(DelayTime));
    }

    private void PlaySound(AudioClip clip)
    {
        if (AudioPlayer != null)
        {
            // Stop the currently playing sound if there is one
            if (AudioPlayer.isPlaying)
            {
                AudioPlayer.Stop();
            }

            // Play the new sound
            if (clip != null)
            {
                AudioPlayer.PlayOneShot(clip);
            }
        }
    }

    private void SetCooldownForAll()
    {
        foreach (OnClickTouch obj in allOnClickTouchInstances)
        {
            if (obj != this) // Skip the current object
            {
                obj._lastClickTime = Time.time; // Set the cooldown time for other objects
            }
        }
    }

    private IEnumerator CallCheckManagerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckManager?.IsMatchingObjects();
    }


public bool CanClikButton = true;

public void CanClick(bool SetClick)
    {
        CanClikButton = SetClick;

    }


}
