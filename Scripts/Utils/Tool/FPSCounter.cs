using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCounter : Singleton<FpsCounter>
{

    // how often should the number update
    public float updateInterval = 0.5f;

    private float fps;
    private float timeleft;
    private int frames = 0;
    private float accum = 0.0f;
    private GUIStyle textStyle = new GUIStyle();


    // [properties]
    public float FPS { get => fps; set => fps = value; }



    #region UNITY
    private void Start()
    {
        timeleft = updateInterval;
        Init();
    }

    private void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // interval ended 
        // update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = (accum / frames);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
    #endregion



    public void Init()
    {
        textStyle.fontSize = 25;
        textStyle.fontStyle = FontStyle.Bold;
        textStyle.normal.textColor = Color.white;
    }


    private void OnGUI()
    {
        // display the fps and round to 2 decimals
        GUI.Label(new Rect(Screen.width - 170, 20, 70, 40), fps.ToString("f1") + " FPS", textStyle);
    }


}
