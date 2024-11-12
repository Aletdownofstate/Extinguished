using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 1;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        canvasGroup.alpha -= Time.deltaTime / 4;
    }
}
