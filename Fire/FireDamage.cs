using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private bool takeDamage;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (takeDamage)
        {
            playerHealth.TakeDamage(25);
        }        
    }    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            takeDamage = true;
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        takeDamage = false;
    }
}