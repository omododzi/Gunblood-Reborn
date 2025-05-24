using System;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas startui;
    public Canvas gameui;

    void Start()
    {
        gameui.enabled = false;
        startui.enabled = true;
        CameraController.starting = true;
    }
    private void FixedUpdate()
    {
        if (CameraController.starting)
        {
            gameui.enabled = false;
            startui.enabled = true;
        }
        else
        {
            gameui.enabled = true;
            startui.enabled = false;
        }
    }
}
