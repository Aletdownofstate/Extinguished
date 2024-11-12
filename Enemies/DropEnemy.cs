using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            rb.isKinematic = false;
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerHealth.TakeDamage(10);
            Destroy(gameObject);
        }
    }    
}