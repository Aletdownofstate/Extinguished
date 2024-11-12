using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject doubleJumpItem;
    public GameObject dashItem;

    // Start is called before the first frame update
    void Awake()
    {
       // Instantiate(doubleJumpItem, new Vector3(-10f, 15.85f), transform.rotation);

       // Instantiate(dashItem, new Vector3(0, 0, 0), transform.rotation);
    }
}