using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, height, startPos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;        
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = transform.position.y + 5f;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);        
        transform.position = new Vector3(startPos + dist, height, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos)
        {
            startPos -= length;
        }
    }
}