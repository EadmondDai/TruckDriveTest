using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// © By Eadmond, 12.19.2016
// Used to adjust the position of the camera.

public class CameraPosChange : MonoBehaviour {

    // Used to change the distance camera moves.
    public float DeltaDistance = 0.2f; 

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Use those key to adjust the camera.

        if( Input.GetButtonDown("CameraUp"))
        {
            transform.Translate(0, DeltaDistance, 0);
        }

        if (Input.GetButtonDown("CameraDown"))
        {
            transform.Translate(0, -DeltaDistance, 0);
        }

        if (Input.GetButtonDown("CameraForward"))
        {
            transform.Translate(0, 0, DeltaDistance);
        }

        if (Input.GetButtonDown("CameraBackward"))
        {
            transform.Translate(0, 0, -DeltaDistance);
        }

        if (Input.GetButtonDown("CameraLeft"))
        {
            transform.Translate(DeltaDistance, 0, 0);
        }

        if (Input.GetButtonDown("CameraRight"))
        {
            transform.Translate(-DeltaDistance, 0, 0);
        }
    }
}
