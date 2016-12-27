using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColManager : MonoBehaviour
{
    // for singleton
    private static ColManager _singleton;
    public static ColManager Singleton { get { return _singleton; } }
    void Awake() { _singleton = this; }
    void OnDestroy() { _singleton = null; }
    // props
    bool headInArea = false;
    bool trailerInArea = false;
    bool objectiveDone = false;
    // funcs

    // Player In Area --> Next Stage
    public void onPlayerInArea(bool isHead)
    {
        Debug.LogWarning("Player in area! isHead=" + isHead);
        if (objectiveDone)
            return;
        if (isHead)
            headInArea = true;
        else
            trailerInArea = true;
        if(headInArea && trailerInArea)
        {
            objectiveDone = true;
            Debug.LogWarning("GameObject achieved!");
            // show info
            UICanvas.Singleton.showInstruction("Successful! Next Stage!");
            // TODO: next stage
        }
    }

    // Player Hit Curb --> Redo Current Stage
    public void onPlayerHitCurb()
    {
        // show fail info
        UICanvas.Singleton.showInstruction("You hit the curb! Failed!");
        //TODO: redo current stage
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void onPlayerHitWall()
    {
        UICanvas.Singleton.showInstruction("You hit the wall! Failed!");
        //TODO: redo current stage
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
