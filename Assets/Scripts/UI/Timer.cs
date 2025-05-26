using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public static float shoottime;
    public static bool Canshoot = false;
    
    public static float timer;
    private Color targetColor;
    
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip fullClip;
    private AudioClip trimmedClip;

    void Start()
    {
        int sampleRate = fullClip.frequency;
        int samplesCount = sampleRate * 2;
        timer = 0f;
        float[] samples = new float[samplesCount * fullClip.channels];
        fullClip.GetData(samples, 0);
        
        trimmedClip = AudioClip.Create(
            fullClip.name + "_trimmed",
            samplesCount,
            fullClip.channels,
            sampleRate,
            false);
            
        trimmedClip.SetData(samples, 0);
        audioSource.clip = trimmedClip;
    }
    private void OnEnable()
    {
        shoottime = Random.Range(5, 15);
        timer = 0f;
        timerText.color= Color.red; // Начальный цвет
        Canshoot = false;
        //Cursor.visible = false;
    }

    private void OnDisable()
    {
        timer = 0f;
    }

    private void Update()
    {
        if (!CameraController.starting)
        {
            CameraController cameraSc = Camera.main.GetComponent<CameraController>();
            if (!cameraSc.buletfound)
            {
                timer += Time.deltaTime;
                timerText.text = timer.ToString("0.00");
            }
            if (timer <= shoottime * 0.5f && timerText.color != Color.red)
            {
                timerText.color = Color.red;
            }
            else if (timer > shoottime * 0.5f && timer < shoottime * 0.9f && timerText.color != Color.yellow)
            {
                timerText.color= Color.yellow;
            }
            else if (timer >= shoottime && timerText.color != Color.green)
            {
                if (CanvasController.Mysic)
                {
                    audioSource.Play();
                }
                Cursor.visible = true;
                timerText.color = Color.green;
                Canshoot = true;
            }
        }
    }
}