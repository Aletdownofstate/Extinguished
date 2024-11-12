using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float height;
    public float speed;
    Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * speed) * height + initPos.y, 0);
    }
}