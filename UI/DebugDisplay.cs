using UnityEngine;
using UnityEngine.UI;

public class DebugDisplay : MonoBehaviour
{
    [SerializeField] private Text hpText;
    [SerializeField] private Text waterText;
    [SerializeField] private Text moveText;
    [SerializeField] private Text shootText;
    [SerializeField] private Text groundedText;
    [SerializeField] private Text jumpText;
    [SerializeField] private Text xVelocity;
    [SerializeField] private Text yVelocity;

    private PlayerController player;
    [SerializeField] private CanvasGroup canvasGroup;

    private string hp;
    private int water;
    private bool move;
    private bool shoot;
    private bool jump;
    private float xvel;
    private float yvel;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();        
    }

    void Update()
    {
        float waterFloat = GameData.currentWater;

        hp = GameData.currentHealth.ToString();
        hpText.text = hp;
        water = (int)(waterFloat + 0.5f);
        waterText.text = water.ToString();
        move = player.controlEnabled;
        jump = player.canJump;
        shoot = GameData.shootEnabled;
        xvel = player.moveHorizontal;
        yvel = player.rb.velocity.y;
        xVelocity.text = xvel.ToString();
        yVelocity.text = yvel.ToString();
        
        // Tab to display debug menu

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (canvasGroup.alpha == 1)
            {
                canvasGroup.alpha = 0;
            }
            else
            {
                canvasGroup.alpha = 1;
            }
        }

        if (shoot)
        {
            shootText.color = Color.green;
        }
        else
        {
            shootText.color = Color.red;
        }

        if (move)
        {
            moveText.color = Color.green;
        }
        else
        {
            moveText.color = Color.red;
        }

        if (player.isGrounded())
        {
            groundedText.color = Color.green;
        }
        else
        {
            groundedText.color = Color.red;
        }

        if (player.canJump)
        {
            jumpText.color = Color.green;
        }
        else
        {
            jumpText.color = Color.red;
        }        
    }
}