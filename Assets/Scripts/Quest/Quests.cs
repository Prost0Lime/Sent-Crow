using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public DialogueManager DM;  //������ ��������
    public DialogueTrigger DT; //������ �������
    public int numDialStartQuest;   //����� ������� ��� ������ ������
    public int numDialStopQuest; //����� ������� ��� ��������� ������

    public GameObject[] prize;

    public int itemsInStart;        //����� ��������� ��� ������
    public List<int> items;         //�� �������� ��� ������

    [HideInInspector]
    public List<int> prizeId;
    [HideInInspector]
    public List<float> prizeX;

    public void Start()
    {
        DT.ndtqst = numDialStartQuest; //������������� ����� ������� ��� ������ ������ 
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (DM.startQuest == true)
        {
            if ((other.tag != "Player") && (other.gameObject.GetComponent<Pickup>().id == items[0]))
            {
                Destroy(other.gameObject);
                CheckQuest();
            }
        }
    }

    public void CheckQuest()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i == 0)
            {
                items.RemoveAt(0);
                DM.continueText = true;
                DT.TriggerDialogue();
                break;
            }

        }

        if ( DT.numToDeleteQueue == numDialStopQuest) //������ ������
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
        DM.startQuest = false;
        DT.continueText = true;
        DT.startQuest = false;
    }

    public void GetIdPrize()        //����� ��������� �� � ����� � �������� ����� ��� ��������� �� �����
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