using System;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas startui;
    public Canvas gameui;
    public GameObject mysicOn;
    public GameObject mysicOff;
    public static bool Mysic = true;

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
    public void Switchmysic()
    {
        Mysic = !Mysic;
        if (!Mysic)
        {
            mysicOff.SetActive(true);
            mysicOn.SetActive(false);
        }
        else
        {
            mysicOff.SetActive(false);
            mysicOn.SetActive(true);
        }
    }
}
