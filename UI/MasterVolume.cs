using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolume : MonoBehaviour
{
    public Slider slider;        

    public float volume = 0.8f;    

    // Start is called before the first frame update
    void Start()
    {
        slider.value = volume;
        AudioListener.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = slider.value;        
    }
}