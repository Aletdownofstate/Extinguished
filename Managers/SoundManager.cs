using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip jumpSound, dashSound, footstepSound, damageSound;
    public static AudioSource audioSrc;    

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        jumpSound = Resources.Load<AudioClip>("jump");
        dashSound = Resources.Load<AudioClip>("dash");
        footstepSound = Resources.Load<AudioClip>("footsteps");
    }

    public static void PlaySound(string clip)
    {
        float pitch = Random.Range(0.9f, 1f);        

        switch (clip)
        {
            case "jump":                
                audioSrc.volume = 0.2f;
                audioSrc.pitch = pitch;
                audioSrc.PlayOneShot(jumpSound);
                break;

            case "dash":                
                audioSrc.volume = 0.15f;
                audioSrc.PlayOneShot(dashSound);
                break;

            case "footsteps":                
                audioSrc.volume = 0.35f;
                audioSrc.loop = true;
                audioSrc.PlayOneShot(footstepSound);
                break;
        }
    }
}