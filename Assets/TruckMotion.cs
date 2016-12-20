using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @ By Yi YIN, 2016
// Controls the movement of the player's truck

public class TruckMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnAccel(float pedal, int shift)
    {
       
    }

    public void OnBrake(float pedal)
    {

    }

    public void OnReverse(float pedal, int shift)
    {

    }

    // angle should be -1 ~ 1. -1 means most left, 1 means most right.
    public void OnTurn(float angle)
    {

    }
}
