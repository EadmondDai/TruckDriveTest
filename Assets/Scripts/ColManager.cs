using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColManager : MonoBehaviour {
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider.name);
    }
    // player hits curb
    void onPlayer_Curb()
    {

    }

    // player hits an AI car
    void onPlayer_Car()
    {

    }


}
