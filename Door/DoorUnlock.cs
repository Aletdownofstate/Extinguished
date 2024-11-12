
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Light2D leftLight;
    [SerializeField] private Light2D rightLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(OpenDelay());
    }

    IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(5.5f);
        door.isDoorLocked = false;
        door.doorLatch.Play();
        leftLight.color = Color.green;
        rightLight.color = Color.green;
        Destroy(gameObject);
    }

}