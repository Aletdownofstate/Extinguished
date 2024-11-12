using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePush : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource crateSrc;

    public ParticleSystem dust;
    private bool isPushing = false;    

    // Update is called once per frame
    void Update()
    {
        if (isPushing)
        {
            dust.Play();
        }
        else if (!isPushing)
        {
            dust.Stop();
        }

        if (rb.velocity.x == 0)
        {
            crateSrc.Stop();
            dust.Stop();            
        }

        if (rb.velocity.x > 5 || rb.velocity.x < -5)
        {
            crateSrc.Stop();
            dust.Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && rb.velocity.x != 0 && rb.velocity.y == 0)
        {
            PushSound();
            isPushing = true;
        }        
        else if (rb.velocity.x == 0)
        {
            isPushing = false;            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rb.velocity.x == 0)
        {
            isPushing = false;
            crateSrc.Stop();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        crateSrc.Stop();
        isPushing = false;
    }

    private void PushSound()
    {
        if (crateSrc.isPlaying == false && rb.velocity.x != 0)
        {
            crateSrc.loop = true;
            crateSrc.Play();
        }
    }
}