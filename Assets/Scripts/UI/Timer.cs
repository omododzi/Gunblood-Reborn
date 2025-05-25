using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public Image sprite;
    public static float shoottime;
    public static bool Canshoot = false;
    
    private float timer;
    private Color targetColor;
    private void OnEnable()
    {
        shoottime = Random.Range(5, 15);
        timer = 0f;
        sprite.color = Color.red; // Начальный цвет
        Canshoot = false;
        //Cursor.visible = false;
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
            if (timer <= shoottime * 0.5f && sprite.color != Color.red)
            {
                sprite.color = Color.red;
            }
            else if (timer > shoottime * 0.5f && timer < shoottime * 0.9f && sprite.color != Color.yellow)
            {
                sprite.color = Color.yellow;
            }
            else if (timer >= shoottime * 0.9f && sprite.color != Color.green)
            {
                Cursor.visible = true;
                sprite.color = Color.green;
                Canshoot = true;
            }
        }
    }
}