using System.Collections;
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
        StartCoroutine(DisableAfterDelay(0.1f));
    }
    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < skins.Count; i++)
        {
            skins[i].SetActive(openchange);
        }
    }
}
