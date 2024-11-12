using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCounter : MonoBehaviour
{
    public int noOfFires;

    // Update is called once per frame
    void Update()
    {
        noOfFires = GameObject.FindGameObjectsWithTag("Fire").Length;        
    }
}
