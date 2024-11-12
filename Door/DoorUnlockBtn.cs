using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorUnlockBtn : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Light2D leftLight;
    [SerializeField] private Light2D rightLight;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Use"))
        {
            StartCoroutine(UnlockWait());            
        }
    }

    IEnumerator UnlockWait()
    {
        yield return new WaitForSeconds(1f);
        door.isDoorLocked = false;
        door.doorLatch.Play();
        leftLight.color = Color.green;
        rightLight.color = Color.green;
        Destroy(gameObject);
    }
}
