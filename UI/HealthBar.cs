using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Slider slider;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void Sethealth(int health)
    {
        slider.value = health;        
    }
}