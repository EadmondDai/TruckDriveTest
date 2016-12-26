using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @ By Yi YIN, 2016
// Controls the movement of the player's truck

public class TruckMotion : MonoBehaviour {
    // for Logitech Controller
    LogitechGSDK.LogiControllerPropertiesData properties;

    private WheelCollider[] wheels;

    public float maxAngle = 30;
    public float maxTorque = 300;
    public Transform CarModel;

    public float FrictionRate = 0.7f;

    public float AccelerateRate;
    public float BrakeRate;
    public float ReverseRate;
    public float TurnRate;

    // Use this for initialization
    void Start () {        
        wheels = GetComponentsInChildren<WheelCollider>();

        Debug.Log(LogitechGSDK.LogiSteeringInitialize(false));

        if (!LogitechGSDK.LogiIsConnected(0) || !LogitechGSDK.LogiUpdate())
        {
            Debug.Log("PLEASE PLUG IN A STEERING WHEEL OR A FORCE FEEDBACK CONTROLLER; AND");
            Debug.Log("THIS WINDOW NEEDS TO BE IN FOREGROUND IN ORDER FOR THE SDK TO WORK PROPERLY");
            return;
        }
    }
    
    // Update is called once per frame
    void Update () {
        float deltaTime = Time.deltaTime;
        // using Logitech Wheels
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            // Use this to get wheels control
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            float oilPedal = (32767.0f-rec.lY) / 65536.0f;          // Oil pedal --> (0,1)
            float wheelDirection = rec.lX / 32768.0f;               // Wheel direction --> [-1,1)
            float brakePedal = (32767.0f - rec.lRz) / 65536.0f;     // Brake pedal
            //Debug.Log(" lrz : " + rec.lRz.ToString() + " minus :" + minusVertical.ToString());
            OnAccel(oilPedal, 0);
            OnTurn(wheelDirection);
            if(GetComponent<Rigidbody>().velocity.magnitude>0.1f)
                OnBrake(brakePedal);
            else
                OnReverse(brakePedal, 0);
        }
        // using keyboard
        else
        {
            // Oil Pedal: Up, W
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                OnAccel(1, 0);
            else
                OnAccel(0, 0);
            // Brake Pedal: Down, S
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
                    OnBrake(1);
                else
                    OnReverse(1, 0);
            }
            else {
                OnBrake(0);
                OnReverse(0,0);                
            }
            // Wheel
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                OnTurn(-1);
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                OnTurn(1);
            else
                OnTurn(0);
        }
    }

    // Update PhyX
    void FixedUpdate()
    {
        foreach (WheelCollider wheel in wheels)
        {
            // a simple car where front wheels steer while rear ones drive
            if (wheel.transform.localPosition.z > 0)
                wheel.steerAngle = TurnRate*maxAngle;

            if (wheel.transform.localPosition.z < 0)
                wheel.motorTorque = (AccelerateRate-BrakeRate) * maxTorque;

            // Handle break and reverse.

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

    // angle should be -1 ~ 1. -1 means most left, 1 means most right.
    public void OnTurn(float angle)
    {
        TurnRate = angle;
    }
}
