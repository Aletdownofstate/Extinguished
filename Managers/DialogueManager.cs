using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private PlayerController player;
    private Queue<string> sentences;
    public CanvasGroup canvasGroup;
    public DialogueTrigger dialogueTrigger;
    public Text dialogueText;
    public Animator animator;

    private bool isTextAnimating = false;
    private bool skipTypewriterEffect = false;
    private bool textFinished = false;
    private bool inCutscene;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();        
        sentences = new Queue<string>();
    }

    private void Update()
    {
        inCutscene = GameData.inCutscene;

        //if (!dialogueTrigger.isSkippable)
        //{
        //    if (player.controlEnabled)
        //    {
        //        animator.SetBool("IsOpen", false);
        //        dialogueTrigger.gameObject.SetActive(false);
        //    }
        //}       

        if (isTextAnimating && Input.GetButtonDown("Jump"))
        {
            skipTypewriterEffect = true;
        }

        if (textFinished && Input.GetButtonDown("Jump"))
        {
            DisplayNextSentence();
        }

        if (inCutscene)
        {
            player.controlEnabled = false;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        canvasGroup.alpha = 1;
        animator.SetBool("IsOpen", true);
        
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTextAnimating)
        {
            skipTypewriterEffect = true;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));        
    }

    void EndDialogue()
    {        
        animator.SetBool("IsOpen", false);
        StartCoroutine(DialogueEndCooldown());        
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTextAnimating = true;
        textFinished = false;
        dialogueText.text = "";

        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text += sentence[i];

            if (!skipTypewriterEffect)
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
        isTextAnimating = false;       
        skipTypewriterEffect = false;
        textFinished = true;
    }

    IEnumerator DialogueEndCooldown()
    {        
        animator.SetBool("IsOpen", false);
        yield return new WaitForSeconds(0.5f);
        player.controlEnabled = true;
        canvasGroup.alpha = 0;
    }
}