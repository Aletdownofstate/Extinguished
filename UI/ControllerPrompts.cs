using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPrompts : MonoBehaviour
{
    [SerializeField] private Image north;
    [SerializeField] private Image south;
    [SerializeField] private Image east;
    [SerializeField] private Image west;

    public GameObject uiPrompt;    

    [SerializeField] private CanvasGroup uiGroup;
    private bool fadeIn = false;
    private bool fadeOut = false;    

    [SerializeField] ButtonColors buttonColors = new ButtonColors();    
    enum ButtonColors
    {
        Yellow,
        Green,
        Red,
        Blue,
        Grey
    };

    private void Start()
    {
        north = GameObject.FindGameObjectWithTag("NorthBtn").GetComponent<Image>();
        south = GameObject.FindGameObjectWithTag("SouthBtn").GetComponent<Image>();
        east = GameObject.FindGameObjectWithTag("EastBtn").GetComponent<Image>();
        west = GameObject.FindGameObjectWithTag("WestBtn").GetComponent<Image>();
        uiGroup = GameObject.Find("Controller Prompts").GetComponent<CanvasGroup>();        
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (uiGroup.alpha < 1)
            {
                uiGroup.alpha += Time.deltaTime;
                if (uiGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (uiGroup.alpha >= 0)
            {
                uiGroup.alpha -= Time.deltaTime;
                if (uiGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("Player"))
        {
            NorthPrompt();
            SouthPrompt();
            EastPrompt();
            WestPrompt();            
            ShowUI();
            StartCoroutine(Deactivate());
        }
    }

    private void NorthPrompt()
    {        
        if (buttonColors == ButtonColors.Yellow)
        {
            north.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            north.GetComponent<Image>().color = Color.grey;
        }
    }   

    private void SouthPrompt()
    {
        if (buttonColors == ButtonColors.Green)
        {
            south.GetComponent<Image>().color = Color.green;
        }
        else
        {
            south.GetComponent<Image>().color = Color.grey;
        }     
    }

    private void EastPrompt()
    {
        if (buttonColors == ButtonColors.Red)
        {
            east.GetComponent<Image>().color = Color.red;
        }
        else
        {
            east.GetComponent<Image>().color = Color.grey;
        }
    }

    private void WestPrompt()
    {
        if (buttonColors == ButtonColors.Blue)
        {
            west.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            west.GetComponent<Image>().color = Color.grey;
        }
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(4f);
        HideUI();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void ShowUI()
    {
        fadeIn = true;        
    }

    public void HideUI()
    {
        fadeOut = true;        
    }

    private string GetControllerType()
    {
        string[] joystickNames = Input.GetJoystickNames();

        foreach (string joystickName in joystickNames)
        {
            if (joystickName.ToLower().Contains("xbox"))
            {
                Debug.Log("Xbox");
                return "XBOX";
            }
            else if (joystickName.ToLower().Contains("playstation"))
            {
                Debug.Log("PS");
                return "PS";
            }
            else if (joystickName.ToLower().Contains("stadia"))
            {
                Debug.Log("Stadia");
                return "Stadia";
            }
            else
            {
                return "OTHER";
            }
        }
        return "OTHER";
    }
}