using UnityEngine;
using System.Collections;

//© Eadmond, 12.14.2016
// This is for testing the moving ability of our truck.

public class MoveTest : MonoBehaviour {

    LogitechGSDK.LogiControllerPropertiesData properties;

    public float MoveSpeed = 10;
    public float ReverseSpeed = 10;
    public float RotateAngle = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;

        // Use this to get wheels control
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);

        float verticalMove = - (rec.lY - 32767) / 10000; // To get only positive number
       
        // Check if the truck is moving.
        if(verticalMove != 0)
        {
            // Apply the brake
            transform.Translate(transform.forward * verticalMove * MoveSpeed * deltaTime);



            float horizontalMove = rec.lX / 10000;
            transform.Rotate(0, horizontalMove * deltaTime * RotateAngle, 0);
            transform.FindChild("TruckTrans").Rotate(0, horizontalMove * deltaTime * RotateAngle, 0);
            //transform.FindChild("Main Camera").Rotate(0, horizontalMove * deltaTime * RotateAngle, 0);
        }
        else
        {
            // Reverse
            float brakeMove = (rec.lRz - 32767) / 10000 / 2; // Move slower
            transform.Translate(transform.forward * brakeMove * ReverseSpeed * deltaTime);
        }

	}
}
