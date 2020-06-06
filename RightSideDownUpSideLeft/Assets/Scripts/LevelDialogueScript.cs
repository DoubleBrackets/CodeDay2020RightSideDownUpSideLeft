using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelDialogueScript : MonoBehaviour
{
    private string message;

    Text dialogueText;

    public Text indicatorText;

    bool isTextOpened = false;
    bool isWithinDistance = false;


    // Start is called before the first frame update
    void Start()
    {
        dialogueText = gameObject.GetComponent<Text>();
        message = dialogueText.text;
        dialogueText.text = "";
        PlayerInteractScript.playerInteractScript.InteractButtonEvent += OnInteractPress;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isWithinDistance = true;
            indicatorText.text = "[E]";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isWithinDistance = false;
            if(isTextOpened)
            {
                isTextOpened = false;
                HideDialogue();
            }
            else
            {
                indicatorText.text = "";
                HideDialogue();
            }
        }
    }

    public void OnInteractPress()
    {
        if(isWithinDistance)
        {
            if (!isTextOpened)
            {
                isTextOpened = true;
                DialogueDisplay();
            }
            else
            {
                isTextOpened = false;
                HideDialogue();
            }
        }
    }

    void DialogueDisplay()
    {
        dialogueText.text = message;
        indicatorText.text = "";
    }

    void HideDialogue()
    {
        if(isWithinDistance)
            indicatorText.text = "[E]";
        dialogueText.text = "";
    }
}
