using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProcess : MonoBehaviour {
    void Start(){
        Debug.Log("Started Curb.");
    }
    // c is the other side of this collision (the Player, for the most times)
    void OnTriggerEnter(Collider c)
    {
        // we ignore non-player events
        if (c.tag != "Player" && c.tag!="Trailer")
            return;
        Debug.LogWarning("Collision between " + c.tag + " and " + gameObject.tag);

        // player X curb/wall
        if (gameObject.tag == "Curb")
            PlayerHitsCurb();       
        else if (gameObject.tag == "Wall")
            PlayerHitsWall();
    }
    // for Objective VS Player/Trailer
    void OnTriggerStay(Collider c)
    {
        // Filter: Objective VS Player/Trailer
        if (gameObject.tag != "Objective" || c.tag != "Player" && c.tag != "Trailer")
            return;
        // main
        if (c.tag == "Player" && c.name == "Head" && gameObject.tag == "Objective")
            PlayerHitsObjective(c);
        else if (c.tag == "Trailer" && c.name == "Body" && gameObject.tag == "Objective")
            TrailerHitsObjective(c);
    }

    void PlayerHitsCurb()
    {
        Debug.LogWarning("Player Hits Curb!");
        ColManager.Singleton.onPlayerHitCurb();
    }
    void PlayerHitsWall()
    {
        Debug.LogWarning("Player Hits Wall!");
        ColManager.Singleton.onPlayerHitWall();
    }

    void PlayerHitsObjective(Collider c)
    {
        // find truck head box collider
        BoxCollider headBox = c.gameObject.GetComponent<BoxCollider>();
        Debug.Assert(headBox);
        // find objective 
        BoxCollider objBox = gameObject.GetComponent<BoxCollider>();
        // calc: objective box contains head box
        if(objBox.bounds.Contains(headBox.bounds.min) && objBox.bounds.Contains(headBox.bounds.max))
            ColManager.Singleton.onPlayerInArea(true);
    }
    void TrailerHitsObjective(Collider c)
    {
        // find trailer box
        BoxCollider trailerBox = c.gameObject.GetComponent<BoxCollider>();
        Debug.Assert(trailerBox);
        // find objective
        BoxCollider objBox = gameObject.GetComponent<BoxCollider>();
        if (objBox.bounds.Contains(trailerBox.bounds.max) && objBox.bounds.Contains(trailerBox.bounds.min))
            ColManager.Singleton.onPlayerInArea(false);
    }
}
