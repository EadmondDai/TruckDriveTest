using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {
    // singleton
    private static UICanvas _singleton;
    public static UICanvas Singleton { get { return _singleton; } }
    void Awake() { _singleton = this; }
    void OnDestroy() { _singleton = null; }

    // children names must be put here!
    string[] childrenNames = new string[] {"TxtInstruction", "BtnNextStage" };

    // Hide Ui with name
    void HideUi(string name, Transform from=null)
    {
        if (!from)
            from = gameObject.transform;
        Transform ch = from.Find(name);
        Debug.Assert(ch);
        ch.gameObject.SetActive(false);
        for(int i = 0; i < ch.childCount; i++)
        {
            Transform chch = ch.GetChild(i);
            Debug.Assert(chch);
            HideUi(chch.name, ch);
        }
    }

	// Set the visibility of the children
	void Start () {
        // turn off all UI elements
        foreach(string name in childrenNames)
            HideUi(name);
	}
	
    public void showInstruction(string s)
    {
        Transform ins = gameObject.transform.Find(childrenNames[0]);
        Debug.Assert(ins);
        if (!ins.gameObject.activeSelf)
            ins.gameObject.SetActive(true);
        ins.gameObject.GetComponent<Text>().text = s;
    }
}
