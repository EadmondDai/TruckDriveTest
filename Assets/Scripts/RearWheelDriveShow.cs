﻿using UnityEngine;
using System.Collections;

// © By Eadmond, 12.17.2016
// This script controls the wheels of the truck.

public class RearWheelDriveShow : MonoBehaviour {

    LogitechGSDK.LogiControllerPropertiesData properties;

    private WheelCollider[] wheels;

	public float maxAngle = 30;
	public float maxTorque = 300;
    public Transform CarModel;

    public float MoveSpeed = 10;
    public float ReverseSpeed = 10;
    public float RotateAngle = 10;

    // here we find all the WheelColliders down in the hierarchy
    void Start()
	{
		wheels = GetComponentsInChildren<WheelCollider>();

        Debug.Log(LogitechGSDK.LogiSteeringInitialize(false));
    }

	// this is a really simple approach to updating wheels
	// here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
	// this helps us to figure our which wheels are front ones and which are rear
	void Update()
	{
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {

            float deltaTime = Time.deltaTime;

            // Use this to get wheels control
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            float verticalMove = -(rec.lY - 32767) / 10000; // To get only positive number
            float horizontalMove = rec.lX / 1000;
            float minusVertical = -(rec.lRz - 32767) / 10000;
            //Debug.Log(" lrz : " + rec.lRz.ToString() + " minus :" + minusVertical.ToString());

            float angle = maxAngle * horizontalMove * deltaTime;
            float torque = maxTorque * (verticalMove - minusVertical) * deltaTime; 

            foreach (WheelCollider wheel in wheels)
            {
                // a simple car where front wheels steer while rear ones drive
                if (wheel.transform.localPosition.z > 0)
                    wheel.steerAngle = angle;

                if (wheel.transform.localPosition.z < 0)
                    wheel.motorTorque = torque;

                // update visual wheels if any
                {
                    Quaternion q;
                    Vector3 p;
                    wheel.GetWorldPose(out p, out q);

                    // assume that the only child of the wheelcollider is the wheel shape
                    Transform shapeTransform = CarModel.FindChild(wheel.name);
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
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
}
