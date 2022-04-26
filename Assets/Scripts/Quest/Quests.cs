using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public DialogueManager DM;  //������ ��������
    public DialogueTrigger DT; //������ �������
    public int numberDialogueStartQuest;
    public int prizeNumberOrStop; //����� ������ ��� �����
    private int num; //����� ��������� ��������

    public GameObject[] prize;

    public int[] items;


    public List<int> prizeId;
    public List<float> prizeX;

    public void Start()
    {
        DT.ndtqst = numberDialogueStartQuest; //������������� ����� ������� ��� ������ ������ 
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (DM.startQuest == true)
        {
            if (other.tag != "Player" && other.gameObject.GetComponent<Pickup>().id == items[num])
            {
                num ++;
                Destroy(other.gameObject);
                CheckQuest();
            }
        }
    }

    public void CheckQuest()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (i == num)
            {
                DM.continueText = true;
                DT.TriggerDialogue();
                break;
            }
        }

        if (num == prizeNumberOrStop) //������ ������
        {
            for (int i = 0; i < prize.Length; i++)
            {
                if (prize[i] != null)
                {
                    prize[i].SetActive(true);
                }
            }

            StopQuest();  
        }
    }

    public void StopQuest()
    {
        DM.continueText = true;
        DM.DisplayControlSentence(); //����������� �� ����� ������
        DM.startQuest = false;
       
    }

    public void GetIdPrize()                     //����� ��������� �� � ����� � �������� ����� ��� ��������� �� �����
    {
        prizeId.Clear();
        prizeX.Clear();

        for (int i = 0; i < prize.Length; i++)
        {
            if (prize[i].GetComponent<Pickup>())        //�������� �� �������� � ��������
            {
                prizeId.Add(prize[i].GetComponent<Pickup>().id);
                prizeX.Add(prize[i].transform.position.x);
            }
        }
    }
}