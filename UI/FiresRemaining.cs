using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiresRemaining : MonoBehaviour
{
    public Text fires;
    private FireCounter fireCounter;
    public CanvasGroup canvasGroup;
    public bool uiFade;

    private void Start()
    {
        canvasGroup.alpha = 0;
        fireCounter = FindObjectOfType<FireCounter>();        
    }

    // Update is called once per frame
    void Update()
    {
        fires.text = fireCounter.noOfFires.ToString();
    }

    private void FixedUpdate()
    {
        if (uiFade)
        {
            canvasGroup.alpha += Time.deltaTime / 0.35f;
            StartCoroutine(FadeFiresCount());
        }
        else
        {
            canvasGroup.alpha -= Time.deltaTime / 1;
        }
    }

    IEnumerator FadeFiresCount()
    {
        yield return new WaitForSeconds(5f);
        uiFade = false;
    }
}