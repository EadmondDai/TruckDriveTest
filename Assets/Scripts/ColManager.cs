using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script access truck control script.
// This script access ErrorMassage script.

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
    string[] stageNames = new string[] { "Maps1_1", "Maps1_2", "Maps1_3", "Maps1_4", "Maps2_1", "Maps2_2", "Maps2_3", "Maps2_4", "Maps0"};
    // funcs


    public TruckControl TruckControlScript;
    public ErrorMassage ErrorMassageScript;

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
        if(currentLevel()==1 && headInArea || headInArea && trailerInArea)
        {
            objectiveDone = true;
            Debug.LogWarning("GameObject achieved!");
            // show info
            UICanvas.Singleton.showInstruction("Successful! Next Stage!");

            ErrorMassageScript.Success();
            // next stage
            nextStage();
        }
    }

    // Player Hit Curb --> Redo Current Stage
    public void onPlayerHitCurb()
    {
        // show fail info in not vr Mode.
        UICanvas.Singleton.showInstruction("You hit the curb! Failed!");

        // show fail info in VR mode.
        ErrorMassageScript.YouHitTheCurb();

        // redo current stage
        reloadStage();
    }
    public void onPlayerHitWall()
    {
       
        UICanvas.Singleton.showInstruction("You hit the wall! Failed!");

        ErrorMassageScript.HitTheWall();

        // redo current stage
        reloadStage();
    }
    // level is 1 or 2, stage is the number after level.
    // e.g. for 1-2, level is 1 and stage is 2.
    int currentLevel()
    {
        string name = SceneManager.GetActiveScene().name;
        for (int i = 0; i < name.Length; i++)
            if (name[i] >= '1' && name[i] <= '9')
                return name[i] - '0';
        Debug.Assert(false,"ColManager.currentLevel(): can NOT find level number.");
        return -1;
    }
    void reloadStage()
    {
        // Before restart the scene, reset the light of the truck.
        TruckControlScript.ResetLight();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
    void nextStage()
    {
        // Before load the scene, reset the light of the truck.
        TruckControlScript.ResetLight();

        string curName = SceneManager.GetActiveScene().name;
        int i = 0;
        for (; i < stageNames.Length; i++)
            if (stageNames[i] == curName)
                break;
        i += 1;
        if (i >= stageNames.Length)
            i = 0;
        SceneManager.LoadScene(stageNames[i], LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(stageNames[i]));
    }
}
