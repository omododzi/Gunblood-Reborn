using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CanvasController : MonoBehaviour
{
    public Canvas startui;
    public Canvas gameui;
    public GameObject mysicOn;
    public GameObject mysicOff;
    public static bool Mysic = true;
    public static bool Win = false;

    void Start()
    {
        gameui.enabled = false;
        startui.enabled = true;
        CameraController.starting = true;
    }
    private void FixedUpdate()
    {
        if (Win)
        {
            Win = false;
            CameraController.starting = true;
            Timer.timer = 0f;
            StartCoroutine(Reload());
        }
        if (CameraController.starting || Win)
        {
            gameui.enabled = false;
            startui.enabled = true;
            Timer.Canshoot = false;
            Win = false;
            CameraController.starting = true;
            Timer.timer = 0f;
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

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
