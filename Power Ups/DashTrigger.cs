using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<PlayerController>().dashEnabled = true;        
        Destroy(gameObject);
    }
}