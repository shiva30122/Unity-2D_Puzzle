using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Events;

// Dev - Shar-Mi( Siva_Ball ) // Make Sure to Visit MY Git_Hub AND You_Tube And !.....
// https://www.youtube.com/@Sharmiyt_  
// https://github.com/shiva30122/Unity-2D_Puzzle

// If You Found This UseFull Then LIKE IT !>>>>>>...........

public class SwapHolder : MonoBehaviour

{

    [Space(2)]
    [Header(" Set TargetPlaceHolder To Swap The Object ")]
    [Header(" With Different Possiblities On ")]
    [Header(" Different Condition !.. ")]
    // Naming Conventions Is Sooooo Hard !..... BUT COMPLETED !....
    [Space(3f)]

    public Transform Target_FULL_Holder; // It Is Used For Swap All Objeccts !..
    public Transform FULL_Path_Point;

    public float Full_Radius;

    
    [Space(8f)]


    // It Is Used For Swap On A Particular Objects To Different Position !.
    public Transform Target_HALF_Holder; 
    public Transform HALF_Path_Point;

    public float Half_Radius;


    [Space(8f)]


    public Transform Target_CROSS_Holder; // It Is Used For Swap On Different Direction !..
    public Transform CROSS_Path_Point;

    public float Cross_Radius;

    [Space(8f)]


    public SwapMoveMent Set_Path;



    [Space(2)]
    [Header(" Set Current Object Which Can ")]
    [Header(" SwapAble To Any Object !.")]
    [Space(3f)]


    public GameObject CurrentObject ;

    [Space(2)]
    [Header(" Check The Input Is Matched !.. ")]
    [Space(3f)]

    public string CheckInputName;

    public string CrossInputName;
    public string InputName;


    [Space(2)]
    [Header(" Set The Name AND Check The Name ")]
    [Space(3f)]

    public string CurrentHolderName; // For Checking To Unlock Event !! Like Currentname == CheckName 
    public ObjectNameCheck CheckName;// match both to check all name == same name !..




    //public SwapControler swapControler; // i dont know why i used ? it ?
    // After FEW HOUSRS IT USED FOR CHANGING THE PATHE OF THE OBJECT !....
    
    [Space(2)]
    [Header(" Bool Variables 0 : 1  ")]
    [Space(3f)]

    public bool IsSwapFuctionManager;

    public bool MatchName;

    public bool CanClick;

    public bool IsMoving;  // TODO or MAKE ALL CKICK SECOND == CURRENT DELAY FOR CLICK !>>...???...


    [Space(2)]
    [Header(" Master Controler For Touch Click  ")]
    [Header(" Set Enable The IsSwapFuctionManager To Use !.. ")]

    [Space(3f)]

    // For loop to make all function with cross check INPUT !...
    public List<GameObject>  AllHolderObjects; 



    private void Start()
    {        
        print(" HI ");
    }


#region   ------     All SWAPPING FUNCTION ()  !...   -------  


    public void FullSwap()
    {
        if(CurrentObject == null || Set_Path == null)
        {
            return;
        }
        if (Target_FULL_Holder == null) return;
        // if (Target_FULL_Holder == null) {
        // print("  NOT APPLICABLE   ");
        // return;
        // }
        IsMatchingObjects();

        //Set_Path = CurrentObject.GetComponent<SwapMoveMent>();
        

        CurrentObject.transform.position = Target_FULL_Holder.transform.position;


        Set_Path.StartMovement(FULL_Path_Point,Full_Radius);




    }

    public void HalfSwap()
    {
        if(CurrentObject == null || Set_Path == null)
        {
            return;
        }
        if (Target_HALF_Holder == null) return;


        //Set_Path = CurrentObject.GetComponent<SwapMoveMent>();

        IsMatchingObjects();


        CurrentObject.transform.position = Target_HALF_Holder.transform.position;


        Set_Path.StartMovement(HALF_Path_Point,Half_Radius);


    }

    public void CrossSwap()
    {
        if(CurrentObject == null || Set_Path == null)
        {
            return;
        }
        if (Target_CROSS_Holder == null) return;

        //Set_Path = CurrentObject.GetComponent<SwapMoveMent>();

        IsMatchingObjects();


        CurrentObject.transform.position = Target_CROSS_Holder.transform.position;


        Set_Path.StartMovement(CROSS_Path_Point,Cross_Radius);



    }

#endregion


#region   ------     COLLITION DECTION ()  !...       -------

    public Transform TempPosition;



    // This method is called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // No Need Of Is Moving Because It Can Move Fater Than LIGTH So NO Problem
        // UNTILL IF IT IS NOT A CIRCLE OR SWAPING THAT CAN MAKE COLIDE WIT NOT MOVING OBJECT !... OR DONTKNOW !...
        



            print("  Colided      "+ other.GetInstanceID());

            CheckName = other.GetComponent<ObjectNameCheck>();
            
            //Set_Path = other.GetComponent<SwapMoveMent>();

            

            if (CheckName)
            {

                CurrentObject = other.GameObject();

                Set_Path = other.GetComponent<SwapMoveMent>();


                print(" CheckName  Current Object :  "+CheckName.ObjectName+"   "+ CurrentObject+"SetPath "+Set_Path);

                if (CheckName.ObjectName == CurrentHolderName)
                {
                    MatchName = true;
                }
                else MatchName = false;


            }


            // TempPosition.transform.position = other.transform.position;
            // TempPosition.transform.position = SetPosition.TempPosition.transform.position;

            //print("  TEMP COLLIDED TO SET POSITION  "+ TempPosition.transform.position);



            //not included !...
            Vector3 otherObjectPosition = other.transform.localPosition;

            Debug.Log("Colliding Object Position: " + otherObjectPosition+ " GameObject Name "+ CheckName.ObjectName );

        
    }

#endregion


#region   ------     TOUCH EVENT () For MasterCountroller  !...             -------


    private int NumberOfLoops = 0;
    

    public void OnClickFunctionExecuted(string ExecuteInputName)


    {    

        if (UNLOCK.activeSelf)
        {
            return;
        }



        //Enable For Use Touch Countroler BOOL That Make EASY To Acces All Objects !......
        
        if(IsSwapFuctionManager == false)
        {
            AllHolderObjects = null;
        }

        if(AllHolderObjects == null) return;

        if (AllMatched == true) return;

        
        NumberOfLoops = 0;
        
        print(" Geted InputName TO SWAPMANAGER : " + ExecuteInputName);

        foreach (GameObject obj in AllHolderObjects)   // All SwapHolder OBJECTS !....
        {

            // Ensure the GameObject has the component
            SwapHolder CurrentSetFunction = obj.GetComponent<SwapHolder>();


            if (CurrentSetFunction == null) 
            {
                Debug.Log(" CurrentSetFunction NotFount Check In ALLOBJECTS !.... CODE 404 ");
                return; // Just Claify if 
            }

            NumberOfLoops+=1; // TESTING 



            if (CurrentSetFunction != null)

            {


                // Perform actions based on ExecuteInputName

                if (ExecuteInputName == CurrentSetFunction.InputName)
                {
                    CurrentSetFunction.HalfSwap(); // take too much of time to identify the logic glitch
                }
                else if (ExecuteInputName == CurrentSetFunction.InputName)   // for using difrerent function !....
                {
    
                    CurrentSetFunction.HalfSwap(); 
                }
    
    // take too much of time to identify the logic glitch . Ithink te Number of function Input Taks !?.. OR DONTKNOW !>>>...
    // It Only Works For One Button So IF NEED MORE THEN ADD MORE FUNCTION AS SAME BECAUSE IT ONLY CECK FOR THE INPUT OF THE BUTTON !...
    // OR ELSE ADD (ExecuteInputName == "FullSwap") LIKE THIS ?!!! BOTH ARE SAME !....
    // Any way it using same function , Dont touch it work perferty or else i need to drage all objest to TouchClick !...
      
                
                else if (ExecuteInputName == CurrentSetFunction.CrossInputName)
                {
                    CurrentSetFunction.CrossSwap();
                }
                else if (ExecuteInputName == "FullSwap") CurrentSetFunction.FullSwap();   // added extra !...
               

            }

            IsMatchingObjects();

        

        print(" Geted InputName  : " + ExecuteInputName+ " NumberOfTimes " + NumberOfLoops);
        }
            IsMatchingObjects();

    }



#endregion



#region   ------- CHECK FOR IS MATCHING WITH BOTH HOLDER_NAME == CURRENT_OBJECT_NAME !... ----

 
 // Make An Event To Accuer Like : Unloced Some Objects or Move To Next Level
public bool AllMatched = true;

public GameObject UNLOCK; // Show the a Object which set to FALSE 

public UnityEvent UnlockedFunction; // Make a Function when UNLOCKED 1>>>....

// Add this at the class level if not already present
private bool HasMatchedExecuted = false;

public AudioClip MusicOnAllMatch;

public void IsMatchingObjects()

{

    if(AllMatched == true)return;

    // Start by assuming all objects match
    AllMatched = true;

    foreach (GameObject obj in AllHolderObjects)
    {
        // Get the component from the object
        SwapHolder currentSetFunction = obj.GetComponent<SwapHolder>();

        if (currentSetFunction != null)
        {
            // Check if MatchName is false
            if (!currentSetFunction.MatchName)
            {
                AllMatched = false; // Set AllMatched to false if any MatchName is false
                break; // Exit the loop early
            }
        }
        else
        {  
            // If the component is missing, consider it as not matched
            AllMatched = false;
            break; // Exit the loop early
        }
    }

    // Execute ALLMATCHED() only if AllMatched is true and hasMatchedExecuted is false
    if (AllMatched && !HasMatchedExecuted)
    {
        ALLMATCHED();
        HasMatchedExecuted = true; // Prevent further executions
    }
    else if (!AllMatched)
    {
        Debug.Log("Not all objects matched!");
        HasMatchedExecuted = false; // Reset the flag if not all objects matched
    }


    }
    public void ALLMATCHED()
    {   

        if (IsSwapFuctionManager != true) return;
        

        print("  Manager ALLMATCHED () !>>>>>>>>>>>>>>>>>>>>>>>>>.  ");

        // not called by manager !... 
        //Because it clicked TODO make it for manager or create another bool to not execute another time !...
        
        if (AllMatched == true)
        {
            UNLOCK.SetActive(true);
            UnlockedFunction?.Invoke();

            // TODO: Make AN EVENT !>>>??>?>?>./../.././......

            print("    YOU WIN !.....................   ILOVE YOU 3000000 !..  ");

            print("    YOU WIN !... Moved To NextLevel     ");
            print("    YOU WIN !... Moved To NextLevel     ");
            print("    YOU WIN !... Moved To NextLevel     ");
            print("    YOU WIN !... Moved To NextLevel     ");
            print("    YOU WIN !... Moved To NextLevel     ");
            print("    YOU WIN !... Moved To NextLevel     ");



        }
    }


#endregion


 ////////                      COMPLITED !!>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>.........................................
    
}