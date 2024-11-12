using System.Collections;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Door door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if (!door.isDoorOpen)
            {
                door.isDoorOpen = true;
                StartCoroutine(DoorCooldown());
            }
            if (door.isDoorLocked)
            {
                door.locked = true;
            }
        }
    }

    IEnumerator DoorCooldown()
    {
        yield return new WaitForSeconds(3f);        
        door.isDoorOpen = false;
    }
}