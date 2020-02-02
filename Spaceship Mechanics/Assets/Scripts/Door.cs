using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Door : MonoBehaviour
{
    public bool isUnlocked;

    private void Start()
    {
        UpdateLights();
    }

    public void UpdateLights()
    {
        if (isUnlocked)
        {
            transform.GetChild(0).GetChild(0).GetComponent<Light2D>().color = Color.green;
            transform.GetChild(1).GetChild(0).GetComponent<Light2D>().color = Color.green;
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetComponent<Light2D>().color = Color.red;
            transform.GetChild(1).GetChild(0).GetComponent<Light2D>().color = Color.red;
        }
    }
}