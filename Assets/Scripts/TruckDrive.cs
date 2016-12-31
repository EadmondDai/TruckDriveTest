using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

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
// This script access CarControllerFix script.


public class TruckDrive : MonoBehaviour {

    LogitechGSDK.LogiControllerPropertiesData properties;

    private WheelCollider[] wheels;

    private float FrictionRate = 0.7f;
    private float EngineSpinToTireRate = 26;
    private float EffecieceRemain = 0.88f;
    private float CurrentRPM = 1400;

    // Those are the input staff.
    private float AccelerateRate = 0;
    private float BrakeRate = 0;
    private float ReverseRate = 0;
    private float TurnRate = 0;

    public Rigidbody TruckRigidBody;

    public TruckControl TruckControlScript;

    // Related to speed.
    private float CarSpeed;
    private float CarSpeedShowFacotr = 3;
    private float MaxOutPutStartToDecreaseSpeed = 20; // Mile / Hour
    // lose all the effecience on speed 40 m/h.

    // Default velocity of unity is m/s
    // 1m = 0.000621371 miles
    // 1 h = 3600 seconds.
    // 

    public CarControllerFix CarControlScript;

    // here we find all the WheelColliders down in the hierarchy
    void Start()
	{
        Debug.Log(LogitechGSDK.LogiSteeringInitialize(false));
    }

	// this is a really simple approach to updating wheels
	// here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
	// this helps us to figure our which wheels are front ones and which are rear
	void Update()
	{

	}



    void FixedUpdate()
    {
       
        // Using steering wheel to control.
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {

            float deltaTime = Time.deltaTime;

            // Use this to get wheels control
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            float gas = -(rec.lY - 32767) / (65535.0f); // To get only positive number
            float turn = rec.lX / 32768.0f;
            float brake = -(rec.lRz - 32767) / (65535.0f);

            TruckControlScript.OnTurnSteerWheel(turn);

            Vector3 localVec = transform.InverseTransformDirection(TruckRigidBody.velocity);

            if(brake > 0 )
            {
                if (localVec.z > 0.1)
                {
                    TruckControlScript.OnBrake(true);
                    TruckControlScript.OnReverse(false);
                }
                else
                {
                    TruckControlScript.OnReverse(true);
                    TruckControlScript.OnBrake(false);
                }
            }
            else
            {
                TruckControlScript.OnBrake(false);
                TruckControlScript.OnReverse(false);
            }


            CarControlScript.Move(turn, gas, brake, 0);
        }
        else if (!LogitechGSDK.LogiIsConnected(0))
        {
            // Using Keyboard.
            // pass the input to the car!
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            //float handbrake = Input.GetAxis("Jump");
            CarControlScript.Move(h, v, v, 0.0f);
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
