using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireHealthBar : MonoBehaviour
{
    [SerializeField] private FireHealth fireHealth;
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup canvasGroup;

    public Vector3 offset;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }
    
    void Update()
    {
        float health = fireHealth.fireCurrentHealth;
        float maxHealth = fireHealth.fireMaxHealth;

        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;        

        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

        if (health < maxHealth)
        {
            canvasGroup.alpha = 1;
        }
    }
}