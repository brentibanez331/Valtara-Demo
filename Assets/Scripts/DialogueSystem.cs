using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    bool canDialogue = false;
    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && canDialogue)
        {
            dialogueManager.StartDialogue(dialogue);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Entered Dialogue Area");
            canDialogue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.animator.SetBool("isOpen", false);
            Debug.Log("Exited Dialogue Area");
        }
    }
}
