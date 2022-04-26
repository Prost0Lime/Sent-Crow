using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager DM;
    
    private bool firstPressing;
    public bool enterToTrigger;

    public int ndtqst; //номер диалога дл€ старта квеста (берЄтс€ автоматич)

    public void TriggerDialogue() 
    {
        if (DM.startQuest == true)
        {
            Status();
        }

        else if (DM.startQuest == false)
        {
            if ((DM.sentences.Count != 0) || (firstPressing == true))
            {
                DM.boxAnim.SetBool("boxOpen", true);
                DM.startAnim.SetBool("startOpen", false);
            }
            if (firstPressing == false)
            {
                firstPressing = true;
                DM.StartDialogue(dialogue);
            }
        }
    }

    public void Status()
    {
        if (DM.continueText == true)
        {
            DM.boxAnim.SetBool("boxOpen", true);
            DM.startAnim.SetBool("startOpen", false);
            DM.DisplayControlSentence();
            DM.continueText = false;
        }
        else if (DM.continueText== false)
        {
            DM.boxAnim.SetBool("boxOpen", true);
            DM.startAnim.SetBool("startOpen", false);
            DM.DisplayControlSentence();
        }
       
    }
}
