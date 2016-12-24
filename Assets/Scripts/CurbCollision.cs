using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurbCollision : MonoBehaviour {
    void Start(){
        Debug.Log("Started Curb.");
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
            ColManager.Singleton.PlayerHitsCurb();
    }
}
