using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProfilePreSecond : MonoBehaviour {

    // Attach this to a GUIText to make a frames/second indicator.
    //
    // It calculates frames/second over each updateInterval,
    // so the display does not keep changing wildly.
    //
    // It is also fairly accurate at very low FPS counts (<10).
    // We do this not by simply counting frames per interval, but
    // by accumulating FPS for each frame. This way we end up with
    // correct overall FPS even if the interval renders something like
    // 5.5 frames.

    public float updateInterval = 0.5F;

    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    private float FPS = 60;

    public Text FPSLable;

    void Start()
    {
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            FPS = accum / frames;

            //string format = System.String.Format("{0:F2} FPS", fps);
            //guiText.text = format;

            //if (fps < 30)
            //    guiText.material.color = Color.yellow;
            //else
            //    if (fps < 10)
            //        guiText.material.color = Color.red;
            //    else
            //        guiText.material.color = Color.green;

            //	DebugConsole.Log(format,level);

            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;

            if (FPSLable != null)
            {
                FPSLable.text = string.Format("{0:0.} FPS", FPS);

                if (FPS < 10)
                {
                    FPSLable.color = Color.red;
                }
                else if (FPS < 30)
                {
                    FPSLable.color = Color.yellow;
                }
                else
                {
                    FPSLable.color = Color.green;
                }
            }
        }
    }

    //void OnGUI()
    //{
    //    int w = Screen.width, h = Screen.height;

    //    GUIStyle style = new GUIStyle();

    //    Rect rect = new Rect(w/2, 0, w, h * 4 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h * 4 / 100;

    //    if (FPS < 10)
    //    {
    //        style.normal.textColor = Color.red;
    //    }
    //    else if (FPS < 30)
    //    {
    //        style.normal.textColor = Color.yellow;
    //    }
    //    else
    //    {
    //        style.normal.textColor = Color.green;
    //    }

    //    string text = string.Format("{0:0.} fps", FPS);
    //    GUI.Label(rect, text, style);
    //}


}
