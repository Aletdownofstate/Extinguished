using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoEnd : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;    
    public Animator transition;

    private bool uiFade;

    private void FixedUpdate()
    {
        if (uiFade)
        {
            canvasGroup.alpha += Time.deltaTime / 2;            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {         
        uiFade = true;
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Demo End");
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
