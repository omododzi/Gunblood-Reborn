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
    }
}
