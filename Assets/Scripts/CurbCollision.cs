using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurbCollision : MonoBehaviour {
    void Start(){
        Debug.Log("Started Curb!!!");
    }
    void OnTriggerEnter(Collider c)
    {
        Debug.LogWarning("CurbCol: Hit by "+c.tag);
    }
}
