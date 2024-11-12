using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D fireLight;
    [SerializeField] private FireHealth fireHealth;
    private float intensity;
    private bool canFlicker = true;

    private void Start()
    {
        fireHealth = FindObjectOfType<FireHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        intensity = Random.Range(0.25f, 1) * fireHealth.fireCurrentHealth / 100;

        if (canFlicker)
        {
            StartCoroutine(FlickerRate());            
        }
    }

    private IEnumerator FlickerRate()
    {
        yield return new WaitForSeconds(0.05f);
        fireLight.intensity = intensity + 0.1f;
        canFlicker = true;
    }
}