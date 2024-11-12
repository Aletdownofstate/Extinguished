using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Door : MonoBehaviour
{
    public bool isDoorLocked = false;
    [HideInInspector] public bool locked = false;
    [HideInInspector] public bool isDoorOpen = false;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorClose;
    [SerializeField] private AudioSource doorLocked;
    public AudioSource doorLatch;
    [SerializeField] private Transform door;    
    private GameObject player;
    private Vector3 closedPos;
    private Vector3 openPos;
    private float speed = 0.2f;    

    private void Awake()
    {
        closedPos = transform.position;
        openPos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isDoorOpen && !isDoorLocked)
        {            
            OpenDoor();
        }
        else if (!isDoorOpen)
        {            
            CloseDoor();
        }
        if (locked)
        {
            doorLocked.volume = 0.1f;            
            doorLocked.Play();
            locked = false;
        }
    }

    private void OpenDoor()
    {
        if (transform.position != openPos)
        {
            doorOpen.Play();
            transform.position = Vector3.MoveTowards(transform.position, openPos, speed + Time.deltaTime);            
        }
    }

    private void CloseDoor()
    {
        if (transform.position != closedPos)
        {
            float distance = Vector3.Distance(player.transform.position, door.transform.position);            

            if (distance < 17)
            {
                doorClose.Play();
            }
            transform.position = Vector3.MoveTowards(transform.position, closedPos, speed + Time.deltaTime);            
        }
    }
}