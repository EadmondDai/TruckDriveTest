using UnityEngine;
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

// This script access TruckControl script.



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
    public float AccelerateRate = 0;
    public float BrakeRate = 0;
    public float ReverseRate = 0;
    public float TurnRate = 0;

    public Rigidbody TruckRigidBody;

    public TruckControl TruckControlScript;

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

            float verticalMove = -(rec.lY - 32767) / (65535.0f); // To get only positive number
            float horizontalMoveRate = rec.lX / 32768.0f;
            float minusVertical = -(rec.lRz - 32767) / (65535.0f);

            float angle = maxAngle * horizontalMoveRate * deltaTime;
            float torque = maxTorque * (verticalMove - minusVertical) * deltaTime;

            OnAccel(verticalMove, 0);
            OnTurn(horizontalMoveRate);

            // Need to know if the car is moving forward or moving backward.
            Vector3 velocity = TruckRigidBody.velocity;
            Vector3 localVel = transform.InverseTransformDirection(velocity);
            if (localVel.z > 0.01)
            {
                float brakeRate = -(rec.lRz - 32767) / 65535.0f;
                OnBrake(brakeRate);
            }
            else
            {
                float reverseRate = -(rec.lRz - 32767) / 65535.0f;
                OnReverse(-(rec.lRz - 32767) / 65535.0f, 0);
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
        TruckControlScript.OnBrake(BrakeRate > 0.1f);
        TruckControlScript.OnReverse(ReverseRate > 0.1f);

        foreach (WheelCollider wheel in wheels)
        {
            // a simple car where front wheels steer while rear ones drive
            // localPosition is used to judge if it is front wheel or back wheel.
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = TurnRate * maxAngle;

            // Only back wheels will accelerate.
            if (wheel.transform.localPosition.z < 0)
            {
                if (AccelerateRate > 0.1f)
                    wheel.motorTorque = AccelerateRate * maxTorque * EngineSpinToTireRate * EffecieceRemain / 4;

                if (BrakeRate > 0.1f || ReverseRate > 0.1f)
                {
                    wheel.brakeTorque = BrakeRate * 14969 / 6;
                    wheel.motorTorque = -ReverseRate * maxTorque * EngineSpinToTireRate * EffecieceRemain / 4;
                }  
            }
                

            // If the car is moving forward, step on brake to brake.
            // If the car is still or moving backward, step on brake to reverse.

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
        ReverseRate = 0;
    }

    public void OnReverse(float pedal, int shift)
    {
        ReverseRate = pedal;
        BrakeRate = 0;
    }

    // angle.  Negative means turn left, positive means turn right.
    public void OnTurn(float rate)
    {
        TruckControlScript.OnTurnSteerWheel(rate);
        TurnRate = rate;
    }

}
