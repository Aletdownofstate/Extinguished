using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Slider slider;

    void Start()
    {
        canvasGroup.alpha = 0;
    }

    public void SetMaxWater(int water)
    {
        slider.maxValue = water;
        slider.value = water;
    }

    public void SetWater(float water)
    {
        slider.value = water;
    }
}