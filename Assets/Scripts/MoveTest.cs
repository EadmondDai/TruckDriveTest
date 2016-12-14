using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour {

    public float MoveSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float deltaTime = Time.deltaTime;

        float verticalMove = Input.GetAxis("Vertical");
        transform.Translate(transform.forward * verticalMove * MoveSpeed * deltaTime);
	}
}
