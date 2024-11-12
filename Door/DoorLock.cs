
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Light2D rightLight;
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(OpenDelay());
    }

    IEnumerator OpenDelay()
    {
        player.controlEnabled = false;
        yield return new WaitForSeconds(2f);
        door.isDoorLocked = true;
        door.doorLatch.Play();        
        rightLight.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        player.controlEnabled = true;
        Destroy(gameObject);        
    }

}
