using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueList : MonoBehaviour
{
    public DialogueTrigger[] NPC;
    public DialogueManager DM;

    public void SearchForTheRight()
    {
        for (int i = 0; i < NPC.Length; i++)
        {
            if (NPC[i].enterToTrigger == true)
            {
                DM.numToSkipPastSentences = (NPC[i].numToDeleteQueue) ; //присваивание значения колва реплик для скипа
                DM.continueText = NPC[i].continueText;  //статус для продолжения диалога
                DM.startQuest = NPC[i].startQuest;  //статус начала квеста
                DM.ndtqs = NPC[i].ndtqst;   //номер для старта квеста
                NPC[i].TriggerDialogueStart();   //вызов диалог триггера
                                
            }
        }
    }

    public void SkipSentences()       //скип реплики (параметр+1)
    {
        for (int i = 0; i < NPC.Length; i++)
        {
            if (NPC[i].enterToTrigger == true)
            {
                NPC[i].NumberSkipSentences();
            }
        }
    }
}
