using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager DM;
    [HideInInspector]
    public bool enterToTrigger;

    public int ndtqst; //����� ������� ��� ������ ������ ����
    [HideInInspector]
    public int numToDeleteQueue; //��������� ��������� ������ ����
    [HideInInspector]
    public bool startQuest;
    [HideInInspector]
    public bool continueText;

    public void TriggerDialogueStart() // ����� ������������ ����� ��������
    {
        DM.StartDialogue(dialogue);
        TriggerDialogue();
    }

    public void TriggerDialogue() 
    {
        if (DM.startQuest == true)
        {
            Status();
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
            continueText = false;
        }
        else if (DM.continueText== false)
        {
            DM.boxAnim.SetBool("boxOpen", true);
            DM.startAnim.SetBool("startOpen", false);
            DM.DisplayControlSentence();
        }
    }

    public void NumberSkipSentences()
    {
        if (numToDeleteQueue != dialogue.sentences.Length - 1)      //������ �� ������ �� ����� ����� ��������
        {
            numToDeleteQueue += 1;
        }

        startQuest = DM.startQuest;
        continueText = DM.continueText;
    }
}
