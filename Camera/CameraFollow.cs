using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera cam;

    private Vector3 offset = new Vector3(0f, 2.5f, -10f);
    private float smoothTime = 0.35f;
    private Vector3 velocity = Vector3.zero;

    public bool followPlayer = true;

    void Update()
    {
        if (followPlayer)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}