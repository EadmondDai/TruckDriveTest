using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColManager : MonoBehaviour {
    private static ColManager _singleton;
    public static ColManager Singleton { get { return _singleton; } }
    void Awake() { _singleton = this; }
    void OnDestroy() { _singleton = null; }

    // player hits curb
    public void PlayerHitsCurb()
    {
        Debug.LogWarning("Player hits the curb!");
    }

    // player hits an AI car
    public void PlayerHitsCar()
    {
        Debug.LogWarning("Player hits an AI car!");
    }


}
