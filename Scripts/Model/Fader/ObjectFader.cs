using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFader : MonoBehaviour
{


    public float fadeSpeed, fadeAmount;
    public float originalOpacity;
    public bool DoFade = false;
    public Material[] Mats;



    void Start()
    {
        Mats = GetComponent<Renderer>().materials;
        foreach (Material mat in Mats)
            originalOpacity = mat.color.a;
            

    }

    void Update()
    {
        if (DoFade)
            FadeNow();
        else
            ResetFade();
    }




    private void FadeNow()
    {
        foreach (Material mat in Mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }


    private void ResetFade()
    {
        foreach (Material mat in Mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }
}