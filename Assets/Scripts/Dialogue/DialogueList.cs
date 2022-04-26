using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueList : MonoBehaviour
{
    public DialogueTrigger NPC;
    public DialogueTrigger NPC1;
    public DialogueTrigger NPC2;
    public DialogueTrigger NPC3;
    public DialogueTrigger NPC4;
    public DialogueTrigger NPC5;
    public DialogueTrigger NPC6;
    public int ndtqsl; //номер диалога дл€ старта квеста (берЄтс€ автоматич)

    public void SearchForTheRight()
    {
        if (NPC.enterToTrigger == true)
        {
            NPC.TriggerDialogue();
            ndtqsl = NPC.ndtqst; //номер диалога дл€ старта квеста (берЄтс€ автоматич)
        }  
        else if (NPC1.enterToTrigger == true)
        {
            NPC1.TriggerDialogue();
            ndtqsl = NPC1.ndtqst;
        }
        else if (NPC2.enterToTrigger == true)
        {
            NPC2.TriggerDialogue();
            ndtqsl = NPC2.ndtqst;
        }
        else if (NPC3.enterToTrigger == true)
        {
            NPC3.TriggerDialogue();
            ndtqsl = NPC3.ndtqst;
        }
        else if (NPC4.enterToTrigger == true)
        {
            NPC4.TriggerDialogue();
            ndtqsl = NPC4.ndtqst;
        }
        else if (NPC5.enterToTrigger == true)
        {
            NPC5.TriggerDialogue();
            ndtqsl = NPC5.ndtqst;
        }
        else if (NPC6.enterToTrigger == true)
        {
            NPC6.TriggerDialogue();
            ndtqsl = NPC6.ndtqst;
        }
    }
}
