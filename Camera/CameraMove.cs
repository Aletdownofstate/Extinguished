using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject cameraPoint;
    
    private CameraFollow camFollow;
    private bool moveCamera = false;
    public float duration = 2;

    Vector3 pointPos;

    public bool inCutscene;

    void Start()
    {
        pointPos = cameraPoint.transform.position;
        camFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();

        inCutscene = false;        
    }

    void Update()
    {
        GameData.inCutscene = inCutscene;

        if (moveCamera)
        {
            StartCoroutine(Lerp(pointPos, duration));
            player.controlEnabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.controlEnabled = false;
        player.moveHorizontal = 0;
        player.animator.SetFloat("Speed", 0);
        player.animator.SetBool("isJumping", false);
        player.animator.SetBool("isFalling", false);
        player.animator.Play("Player_Idle");
        camFollow.followPlayer = false;
        moveCamera = true;
    }

    IEnumerator Lerp(Vector3 targetPosition, float duration)
    {
        inCutscene = true;
        yield return new WaitForSeconds(0.5f);
        float time = 0;
        Vector3 startPosition = cam.transform.position;        
        while (time < duration)
        {
            cam.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cam.transform.position = targetPosition;
        moveCamera = false;
        yield return new WaitForSeconds(5f);
        camFollow.followPlayer = true;
        inCutscene = false;
        GameData.inCutscene = inCutscene;
        player.controlEnabled = true;
        Destroy(gameObject);
    }
}