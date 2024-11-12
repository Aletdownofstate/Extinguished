using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    public Text pressAnyKey;    

    private void Start()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        pressAnyKey.color = new Color32(255, 255, 255, 255);
        yield return new WaitForSeconds(0.75f);
        pressAnyKey.color = new Color32(255, 255, 255, 0);
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(Flash());
    }
}