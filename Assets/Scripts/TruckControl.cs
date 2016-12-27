using System.Collections;
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


    // Related to turnning light.
    public float DefaultLightChangeInterval = 0.5f;
    public float NextLightChangeInterval = 0;
    public float DefaultLigthBlinkInterval = 0.3f;
    public float NextLightBlinkInterval = 0;
    public bool LeftTurnLightOn = false;
    public GameObject LeftTurnLightGroup;

    public bool RightTurnLightOn = false;
    public GameObject RightTurnLightGroup;

    public bool IsOnBrake = false;
    public GameObject BrakeLightGroup;

    public bool IsOnReverse = false;
    public GameObject ReverseLightGroup;

    // Related to trailer light control;
    public bool IsTrailerConnected = false;
    public GameObject TrailerLeftTurnLightGroup;
    public GameObject TrailerRightTurnLightGroup;
    public GameObject TrailerBrakeLightGroup;

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

            NextLightChangeInterval -= Time.deltaTime;

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
                    if(NextLightChangeInterval <=0)
                    {
                        if (i == 4) // Right turn.
                        {
                            OnTurnRight();
                        }
                        else if (i == 5) // Left turn.
                        {
                            OnTurnLeft();
                        }
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

        if(RightTurnLightOn)
        {
            NextLightBlinkInterval -= Time.deltaTime;
            if(NextLightBlinkInterval <=0)
            {
                RightTurnLightGroup.SetActive(!RightTurnLightGroup.activeSelf);
                NextLightBlinkInterval = DefaultLigthBlinkInterval;
            }
        }
        else
        {
            RightTurnLightGroup.SetActive(false);
        }

        if (LeftTurnLightOn)
        {
            NextLightBlinkInterval -= Time.deltaTime;
            if(NextLightBlinkInterval <=0)
            {
                LeftTurnLightGroup.SetActive(!LeftTurnLightGroup.activeSelf);
                NextLightBlinkInterval = DefaultLigthBlinkInterval;
            }
            
        }
        else
        {
            LeftTurnLightGroup.SetActive(false);
        }

        BrakeLightGroup.SetActive(IsOnBrake);

        ReverseLightGroup.SetActive(IsOnReverse);

        if(IsTrailerConnected)
        {
            TrailerBrakeLightGroup.SetActive(IsOnBrake);
            TrailerLeftTurnLightGroup.SetActive(LeftTurnLightGroup.activeSelf);
            TrailerRightTurnLightGroup.SetActive(RightTurnLightGroup.activeSelf);
        }
    }

    public void OnTurnSteerWheel(float rate)
    {
        WheelRotationRate = rate;
    }

    public void OnTurnLeft()
    {
        if (RightTurnLightOn)
            RightTurnLightOn = !RightTurnLightOn;

        LeftTurnLightOn = !LeftTurnLightOn;

        NextLightBlinkInterval = 0;
        NextLightChangeInterval = DefaultLightChangeInterval;
    }

    public void OnTurnRight()
    {
        if (LeftTurnLightOn)
            LeftTurnLightOn = !LeftTurnLightOn;

        RightTurnLightOn = !RightTurnLightOn;

        NextLightBlinkInterval = 0;
        NextLightChangeInterval = DefaultLightChangeInterval;
    }

    public void OnBrake(bool isBrake)
    {
        IsOnBrake = isBrake;
    }

    public void OnReverse(bool isReverse)
    {
        IsOnReverse = isReverse;
    }
}
