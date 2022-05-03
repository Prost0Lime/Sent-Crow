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

        if (other.tag == "Player")
        {
            dt.enterToTrigger = true;
            startAnim.SetBool("startOpen", true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            dt.enterToTrigger = false;
            startAnim.SetBool("startOpen", false);
            dm.EndDialogue();
        }
    }
}
