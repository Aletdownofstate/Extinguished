using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    public AudioSource audioSource;

    private bool uiFade;

    void Start()
    {
        canvasGroup.alpha = 0;
        uiFade = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Use") || Input.anyKeyDown)
        {
            uiFade = true;

            StartCoroutine(Transition());            
        }
    }

    private void FixedUpdate()
    {
        if (uiFade)
        {
            canvasGroup.alpha += Time.deltaTime / 2;
            audioSource.volume -= Time.deltaTime / 2;
        }
    }

    IEnumerator Transition()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Room1");
        while (sceneName != "Room1")
        {
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
