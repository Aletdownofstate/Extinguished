using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightCull : MonoBehaviour
{
    private GameObject player;    
    private Light2D light2D;    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        light2D = GetComponent<Light2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, light2D.transform.position);

        if (distance <= 35f)
        {
            light2D.enabled = true;            
        }
        else if (distance > 35f)
        {
            light2D.enabled = false;            
        }
    }
}