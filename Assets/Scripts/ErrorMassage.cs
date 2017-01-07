using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// © By Eadmond, 1.3.2017
// This scirpt is used to show all the massage need to be showen to the player.

public class ErrorMassage : MonoBehaviour {

    public Text ErrorText;
    private string ShowingString = "This is only for test now.";

    public float DefaultShowTime = 5;
    public float ShowTimeLeft = 0;

	// Use this for initialization
	void Start () {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
        ShowTimeLeft -= Time.deltaTime;
        if (ShowTimeLeft < 0)
            Reset();
	}

    public void Reset()
    {
        ShowingString = "";
        ErrorText.text = ShowingString;
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

    public void HitTheWall()
    {
        ShowingString = "You hitted the wall!";
        ShowText();
    }

    public void Success()
    {
        ShowingString = "Good job!";
        ShowText();
    }

    public void CheckLeftWindow()
    {
        ShowingString = "You checked the left window!";
        ShowText();
    }

    public void CheckLeftMirror()
    {
        ShowingString = "You checked the left mirror!";
        ShowText();
    }

    public void CheckRightWindow()
    {
        ShowingString = "You checked the right window!";
        ShowText();
    }

    public void CheckRightMirror()
    {
        ShowingString = "You checked the right mirror!";
        ShowText();
    }

    void ShowText()
    {
        ErrorText.text = ShowingString;
        ShowTimeLeft = DefaultShowTime;
    }
}
