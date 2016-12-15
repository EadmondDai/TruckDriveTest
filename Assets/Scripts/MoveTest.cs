using UnityEngine;
using System.Collections;

//© Eadmond, 12.14.2016
// This is for testing the moving ability of our truck.

public class MoveTest : MonoBehaviour {

    public float MoveSpeed = 10;
    public float RotateAngle = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;

        float verticalMove = Input.GetAxis("Vertical");
        transform.Translate(transform.forward * verticalMove * MoveSpeed * deltaTime);

        // Check if the truck is moving.
        if(verticalMove != 0)
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            transform.Rotate(0, horizontalMove * deltaTime * RotateAngle, 0);
            transform.FindChild("Truck").Rotate(0, horizontalMove * deltaTime * RotateAngle, 0);
            transform.FindChild("Main Camera").Rotate(0, horizontalMove * deltaTime * RotateAngle, 0);
        }

	}
}
