﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// © By Eadmond, 12.26.2016
// This script is used to provide interface for player to control operation like turning light, change view point in truck.
// All the control through logic, except driving the car through 

// This script access CameraPosChange script.
// This script is accessed by RearWheelDriveShow script.

public class TruckControl : MonoBehaviour {

    public CameraPosChange CameraPosScript;

    public Transform SteelWheelTransObj;
    public float WheelRotationRate;
    public float MaxWheelAngle = 90;

	// Use this for initialization
	void Start () {
        Debug.Log(LogitechGSDK.LogiSteeringInitialize(false));
    }

    // Update is called once per frame
    void Update()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            // 
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            for (int i = 0; i < 128; i++)
            {
                if (rec.rgbButtons[i] == 128)
                {
                    // Those are the red buttons on the steering wheels 
                    if (i == 6) // Right top red button. Move Up
                    {
                        CameraPosScript.OnMoveCamera(0, CameraPosScript.DeltaDistance ,0);
                    }
                    else if (i == 7) // Left top red button. Move Down
                    {
                        CameraPosScript.OnMoveCamera(0, -CameraPosScript.DeltaDistance, 0);
                    }
                    else if (i == 19) // Right mid red button. Move Forward.
                    {
                        CameraPosScript.OnMoveCamera(0, 0, CameraPosScript.DeltaDistance);
                    }
                    else if (i == 20) // Left mid red button. Move Backward.
                    {
                        CameraPosScript.OnMoveCamera(0, 0, -CameraPosScript.DeltaDistance);
                    }
                    else if (i == 21) // Right lower red button. Move leftward.
                    {
                        CameraPosScript.OnMoveCamera(-CameraPosScript.DeltaDistance, 0, 0);
                    }
                    else if (i == 22) // Left lower red button. Move rightward.
                    {
                        CameraPosScript.OnMoveCamera(CameraPosScript.DeltaDistance, 0, 0);
                    }

                    // Turnning light.
                    if(i == 4) // Right turn.
                    {
                        
                    }else if(i == 5) // Left turn.
                    {
                        
                    }
                }
            }

        }
        else if (!LogitechGSDK.LogiIsConnected(0))
        {
            Debug.Log("PLEASE PLUG IN A STEERING WHEEL OR A FORCE FEEDBACK CONTROLLER");
        }
        else
        {
            Debug.Log("THIS WINDOW NEEDS TO BE IN FOREGROUND IN ORDER FOR THE SDK TO WORK PROPERLY");
        }
    }

    void FixedUpdate()
    {
        // Rotate the SteerWheel 
        SteelWheelTransObj.eulerAngles = new Vector3(45, 0, -WheelRotationRate* MaxWheelAngle);
    }

    public void OnTurnSteerWheel(float rate)
    {
        WheelRotationRate = rate;
    }

    public void OnTurnLeft(bool LightOn)
    {

    }

    public void OnTurnRight(bool LightOn)
    {

    }
}
