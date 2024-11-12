using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D doorLight;
    private float intensity;
    private bool canFlicker = true;

    // Start is called before the first frame update
    void Start()
    {
        intensity = doorLight.intensity;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float flickerChance = Random.Range(1, 100);

        if (canFlicker)
        {
            if (flickerChance >= 99)
            {
                StartCoroutine(Flicker());
                canFlicker = false;
            }
        }
    }

    IEnumerator Flicker()
    {
        doorLight.intensity = 0.2f;
        yield return new WaitForSeconds(0.2f);
        doorLight.intensity = intensity;
        canFlicker = true;
    }
}
