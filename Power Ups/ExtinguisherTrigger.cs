using UnityEngine;

public class ExtinguisherTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            FindObjectOfType<PlayerController>().shootEnabled = true;
            GameData.shootEnabled = true;
            Destroy(gameObject);
        }
    }
}