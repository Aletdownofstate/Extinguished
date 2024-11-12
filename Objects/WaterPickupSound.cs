using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickupSound : MonoBehaviour
{
    [SerializeField] private AudioSource pickupSound;

    private bool isPickedup;

    private void Start()
    {
        isPickedup = false;
        pickupSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && GameData.currentWater != 100 && !isPickedup)
        {
            pickupSound.volume = 0.25f;
            pickupSound.Play();           
            isPickedup = true;
        }
    }
}
