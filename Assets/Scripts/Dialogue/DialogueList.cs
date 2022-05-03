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
                DM.numToSkipPastSentences = (NPC[i].numToDeleteQueue) ; //������������ �������� ����� ������ ��� �����
                DM.continueText = NPC[i].continueText;  //������ ��� ����������� �������
                DM.startQuest = NPC[i].startQuest;  //������ ������ ������
                DM.ndtqs = NPC[i].ndtqst;   //����� ��� ������ ������
                NPC[i].TriggerDialogueStart();   //����� ������ ��������
                                
            }
        }
    }

    public void SkipSentences()       //���� ������� (��������+1)
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
