using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {
    int fadingState = 0;
    float fadingTime = 1.0f;

	// Update is called once per frame
	void Update () {
        if (fadingState == 0)
            return;
        float dt = Time.deltaTime;
        dt = Mathf.Min(0.01f, dt);
        Color c0 = GetComponent<Image>().color;
        float alpha = c0.a;
        alpha += dt / fadingTime;
        setAlpha(alpha);
	}
    void setAlpha(float a)
    {
        Color color = GetComponent<Image>().color;
        color.a = a;
        GetComponent<Image>().color = color;
    }
    void setRect(Rect r)
    {
        GetComponent<RectTransform>().position = r.position;
        GetComponent<RectTransform>().sizeDelta = r.size;
    }

    // public funcs
    public void FadeIn(float duration=1.0f)
    {
        // set alpha & geo
        setAlpha(0);
        setRect(Camera.current.pixelRect);
        // set state vars
        fadingState = 1;
        fadingTime = duration;
    }
    public void FadeOut(float duration=1.0f)
    {
        setAlpha(1);
        fadingState = -1;
        fadingTime = duration;
    }
}
