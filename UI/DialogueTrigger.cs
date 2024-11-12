using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] private CameraMove cameraMove;
    private DialogueManager dialogueManager;
    public Dialogue dialogue;    
    public bool isSkippable;

    private void Start()
    {
        gameObject.SetActive(true);
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            player.controlEnabled = false;
            player.moveHorizontal = 0;            
            player.animator.SetFloat("Speed", 0);
            player.animator.SetBool("isJumping", false);
            player.animator.SetBool("isFalling", false);
            player.animator.Play("Player_Idle");            
            TriggerDialogue();
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            dialogueManager.animator.SetBool("IsOpen", false);
            player.controlEnabled = true;
            gameObject.SetActive(false);
        }        
    }

    void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}