using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private LevelConnection connection;
    [SerializeField] private string targetScene;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform cam;
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerHealth playerHealth;
    
    private Vector3 offset = new Vector3(0f, 2.5f, -10f);

    public Animator transition;

    private void Start()
    {
        if (connection == LevelConnection.ActiveConnection)
        {
            FindObjectOfType<PlayerController>().transform.position = spawnPoint.position;
            FindObjectOfType<Camera>().transform.position = spawnPoint.position + offset;

            // Checks & sets player direction on load

            if (spawnPoint.transform.localScale.x == -1)
            {
                player.isFacingRight = false;
                player.SceneTransitionFlip();
            }           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameData.currentHealth = playerHealth.currentHealth;

        var player = collision.GetComponent <PlayerController>();
        if (player != null)
        {
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(3f);        
        LevelConnection.ActiveConnection = connection;
        SceneManager.LoadScene(targetScene);
        while (sceneName != targetScene)
        {
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime;
                yield return null;                
            }
        }        
        yield return new WaitForSeconds(3f);        
    }
}