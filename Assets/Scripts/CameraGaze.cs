using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

// © By Eadmond, 1.2.2017
// This script is used to check if the driver have checked each side when making the turn.

public class CameraGaze : MonoBehaviour {

    // These are the diections need to be checked.
    public bool CheckLeftWindow = false;
    public bool CheckLeftMirror = false;
    public bool CheckRightWindow = false;
    public bool CheckRightMirror = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckOutWithName(string name)
    {
        if(name == "LeftWindow")
        {
            CheckLeftWindow = true;
        }else if(name == "LeftMirror")
        {
            CheckLeftMirror = true;
        }
        else if (name == "RightMirror")
        {
            CheckRightMirror = true;
        }
        else if (name == "RightWindow")
        {
            CheckRightWindow = true;
        }
    }

    bool IfCheckLeftSide()
    {
        return CheckLeftMirror && CheckLeftWindow;
    }

    bool IfCheckRightSide()
    {
        return CheckRightMirror && CheckRightWindow;
    }

    void Reset()
    {
        CheckLeftWindow = false;
        CheckLeftMirror = false;
        CheckRightWindow = false;
        CheckRightMirror = false;
    }
}
