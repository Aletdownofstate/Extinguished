using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpTrigger : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<PlayerController>().wallJumpEnabled = true;
        Destroy(gameObject);
    }
}