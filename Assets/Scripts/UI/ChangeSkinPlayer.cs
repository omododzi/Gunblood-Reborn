using UnityEngine;
using System.Collections.Generic;

public class ChangeSkinPlayer : MonoBehaviour
{
    public List<GameObject> skins;
    private bool openchange = false;

    void Start()
    {
        for (int i = 0; i < skins.Count; i++)
        {
            skins[i].SetActive(openchange);
        }
    }

    public void ChangeSkin()
    {
        openchange = !openchange;
       
        for (int i = 0; i < skins.Count; i++)
        {
            skins[i].SetActive(openchange);
        }
    }
}
