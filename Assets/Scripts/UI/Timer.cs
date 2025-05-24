using System;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
   public TMP_Text timerText;
   private float timer;

   private void Update()
   {
      if (!CameraController.starting)
      {
         timer += Time.deltaTime;
         timerText.text = timer.ToString("0.00");
      }
   }
}
