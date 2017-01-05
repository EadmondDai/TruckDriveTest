using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// © By Eadmond, 1.4.2017
// This script is used to manage the logic part of player maneuver judgement.

// This script access ErrorMassager script.
// This script access TruckControl script.
// This script access CameraGaze script.

public class ManeuverManager : MonoBehaviour {

    // This is used to show the massager, all the dynamic massage should be in the script.
    public ErrorMassage ErrorMassagerScript;

    // This is used to read the state of 
    public TruckControl TruckControlScript;

    // This is used to check if the player checked sides before make the turn.
    public CameraGaze CameraGazeScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnterCornerRight()
    {
        // Check if the right turn light is on.
        if(!TruckControlScript.RightTurnLightOn)
        {

        }

        // Check if the checked left window
        // Not sure if this is neccessary.
        

    }

    public void OnEnterCornerLeft()
    {
        if(!TruckControlScript.LeftTurnLightOn)
        {

        }
    }

    public void OnLeaveCornerRight()
    {
        if (!TruckControlScript.RightTurnLightOn)
        {

        }
    }

    public void OnLeaveCornerLeft()
    {
        if (!TruckControlScript.LeftTurnLightOn)
        {

        }
    }

    public void OnRedLightViolation()
    {

    }

    public void OnHitCurb()
    {

    }

    public void OnHitWall()
    {

    }

}
