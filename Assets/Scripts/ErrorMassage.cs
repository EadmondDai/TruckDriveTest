using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// © By Eadmond, 1.3.2017
// This scirpt is used to show all the massage need to be showen to the player.

public class ErrorMassage : MonoBehaviour {

    public Text ErrorText;
    private string ShowingString = "This is only for test now.";

	// Use this for initialization
	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        ShowingString = "";
        ShowText();
    }

    public void DidNotCheckLeftWindow()
    {
        ShowingString = "You didn't check left window!";
        ShowText();
    }

    public void DidNotCheckLeftMirror()
    {
        ShowingString = "You didn't check left mirror!";
        ShowText();
    }

    public void DidNotCheckRightWindow()
    {
        ShowingString = "You didn't check right window!";
        ShowText();
    }

    public void DidNotCheckRightMirror()
    {
        ShowingString = "You didn't check right mirror!";
        ShowText();
    }

    public void YouHitTheCurb()
    {
        ShowingString = "You hitted the curb!";
        ShowText();
    }

    public void TrailerHitTheCurb()
    {
        ShowingString = "Trailer hitted the curb!";
        ShowText();
    }

    public void DidNotStop()
    {
        ShowingString = "You didn't stop at stop sign!";
        ShowText();
    }

    public void RedLightViolation()
    {
        ShowingString = "Red light violation!";
        ShowText();
    }

    public void CrashOtherCar()
    {
        ShowingString = "You carshed other car!";
        ShowText();
    }

    void ShowText()
    {
        ErrorText.text = ShowingString;
    }
}
