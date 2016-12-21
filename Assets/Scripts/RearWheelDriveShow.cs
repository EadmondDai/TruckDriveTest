﻿using UnityEngine;
using System.Collections;

// © By Eadmond, 12.17.2016
// This script controls the wheels of the truck.

// Truck weight  KG.
// Torque  NM.
// Length Meter.
// Newton  F 	= 	m 	⋅ 	a     1 N 	= 	1 kg 	⋅ 	m/s2

// Uinsg Enging PACCAR MX-13 510

// 驱动力=扭矩×变速箱齿比×主减速器速比×机械效率÷轮胎半径（单位：米）
// 通过这个就能计算出施加在轮胎上面的touque.

// Tire 0.5715 Meter
// Torque = 2500 N*M at 1000 ~ 1400 rpm
// Engine output = 340 kw = 0.34 N*M
// 该引擎Trouque 变化大约如下：
// <= 1400rpm 2500 NM 1850 Lb Ft
//    1900rpm 1708 NM 1260 Lb Ft   



public class RearWheelDriveShow : MonoBehaviour {

    LogitechGSDK.LogiControllerPropertiesData properties;

    private WheelCollider[] wheels;

	public float maxAngle = 30;
	public float maxTorque = 300;
    public Transform CarModel;

    public float FrictionRate = 0.7f;
    public float EngineSpinToTireRate = 26;
    public float EffecieceRemain = 0.88f;
    public float CurrentRPM = 1400;

    // Those are the input staff.
    public float AccelerateRate;
    public float BrakeRate;
    public float ReverseRate;
    public float TurnRate;

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

            OnAccel(verticalMove, 0);
            OnTurn(horizontalMove * deltaTime);
            OnBrake(-(rec.lRz - 32767) / 10000);
            OnReverse(-(rec.lRz - 32767) / 10000, 0);

            // Temp method for testing.
            if (Input.GetAxis("Vertical") !=0)
            {
                AccelerateRate = maxTorque * Input.GetAxis("Vertical");
                
            }

            if(Input.GetAxis("Horizontal") != 0)
            {
                TurnRate = maxAngle * Input.GetAxis("Horizontal"); 
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
        foreach (WheelCollider wheel in wheels)
        {
            // a simple car where front wheels steer while rear ones drive
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = TurnRate;

            if (wheel.transform.localPosition.z < 0)
                wheel.motorTorque = AccelerateRate * maxTorque * EngineSpinToTireRate * EffecieceRemain / 4;

            // Handle break and reverse.
            if (wheel.transform.localPosition.z < 0)
                wheel.brakeTorque = BrakeRate * 14969 / 6;

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


    // Controll realated to truck.
    // pedal [ 0 ~ 1];

    public void OnAccel(float pedal, int shift)
    {
        AccelerateRate = pedal;
    }

    public void OnBrake(float pedal)
    {
        BrakeRate = pedal;
    }

    public void OnReverse(float pedal, int shift)
    {
        ReverseRate = pedal;
    }

    // angle.  Negative means turn left, positive means turn right.
    public void OnTurn(float angle)
    {
        TurnRate = angle;
    }

}
