using System.Collections.Generic;
using UnityEngine;

public class OnCilicEvents : MonoBehaviour
{
    
   
    public void StartingGame()
    {
        CameraController.starting = false;
    }

    public void StoppingGame()
    {
        CameraController.starting = true;
        List<GameObject> bullets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bullet"));
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

   
}
