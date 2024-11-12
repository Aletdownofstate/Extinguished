using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomIn : MonoBehaviour
{
    private Camera cam;

    [Range(8.5f, 12f)]
    public float zoomSize;

    void Start()
    {
        cam = Camera.main;      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            StartCoroutine(ZoomIn());
        }        
    }

    IEnumerator ZoomIn()
    {
        while (cam.orthographicSize != zoomSize)
        {
            if (cam.orthographicSize > zoomSize)
            {
                cam.orthographicSize -= 1.5f * Time.deltaTime;                
            }
            else
            {
                cam.orthographicSize = zoomSize;
            }
            yield return new WaitForSeconds(0.0f);
        }
    }
}