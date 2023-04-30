using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public Animator startAnim;
    public DialogueManager dm;
    public DialogueTrigger dt;
    [HideInInspector]
    public SoundManager soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("AM").GetComponent<SoundManager>();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            dt.enterToTrigger = true;
            startAnim.SetBool("startOpen", true);
            soundManager.AudioSound2.clip = soundManager.Dialogue;
            soundManager.AudioSound2.Play();
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
