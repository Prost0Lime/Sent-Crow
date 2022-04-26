using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public Animator startAnim;
    public DialogueManager dm;
    public DialogueTrigger dt;


    public void OnTriggerEnter2D(Collider2D other)
    {
        dt.enterToTrigger = true;

        if (other.tag == "Player")
        {
            startAnim.SetBool("startOpen", true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        dt.enterToTrigger = false;

        if (other.tag == "Player")
        {
            startAnim.SetBool("startOpen", false);
            dm.EndDialogue();
        }
    }
}
