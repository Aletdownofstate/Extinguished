using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;    
    
    private PlayerController player;

    public bool isPaused;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        isPaused = false;
        canvasGroup.alpha = 0;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && !isPaused)
        {
            isPaused = true;
            player.controlEnabled = false;
            canvasGroup.alpha = 1;            
            AudioListener.pause = true;
            Time.timeScale = 0;
        }

        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && isPaused)
        {
            isPaused = false;
            player.controlEnabled = true;
            canvasGroup.alpha = 0;
            AudioListener.pause = false;
            Time.timeScale = 1;
        }

        if (isPaused && (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Joystick1Button6)))
        {
            Application.Quit();            
        }
    }
}