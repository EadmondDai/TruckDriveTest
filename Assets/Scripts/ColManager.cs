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
    // stages
    string[] stageNames = new string[] { "Maps1_1", "Maps1_2" };
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
            // next stage
            nextStage();
        }
    }

    // Player Hit Curb --> Redo Current Stage
    public void onPlayerHitCurb()
    {
        // show fail info
        UICanvas.Singleton.showInstruction("You hit the curb! Failed!");
        // redo current stage
        reloadStage();
    }
    public void onPlayerHitWall()
    {
        UICanvas.Singleton.showInstruction("You hit the wall! Failed!");
        // redo current stage
        reloadStage();
    }
    void reloadStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    void nextStage()
    {
        string curName = SceneManager.GetActiveScene().name;
        int i = 0;
        for (; i < stageNames.Length; i++)
            if (stageNames[i] == curName)
                break;
        if (i >= stageNames.Length)
            i = 0;
        SceneManager.LoadScene(stageNames[i], LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(stageNames[i]));
    }
}
