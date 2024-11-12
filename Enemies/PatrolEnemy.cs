using System.Collections;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public Transform[] patrolPoints;

    private float moveSpeed = 1f;
    public float waitTime;

    int currentPointIndex;

    bool once;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        
        if (currentPointIndex +1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerHealth.TakeDamage(10);
    }
}