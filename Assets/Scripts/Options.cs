using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Options : MonoBehaviour
{

    

    private void Start()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }
    public void SetFullScreen(bool isFull) 
    {
        Screen.fullScreen = isFull;
    }
}
