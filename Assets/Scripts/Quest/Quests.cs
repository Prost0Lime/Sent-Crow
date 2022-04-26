using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public DialogueManager DM;  //диалог менеджер
    public DialogueTrigger DT; //диалог триггер
    public int numberDialogueStartQuest;
    public int prizeNumberOrStop; //номер квеста для приза
    private int num; //номер отданного предмета

    public GameObject[] prize;

    public int[] items;


    public List<int> prizeId;
    public List<float> prizeX;

    public void Start()
    {
        DT.ndtqst = numberDialogueStartQuest; //присваивается номер диалога для старта квеста 
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

        if (num == prizeNumberOrStop) //выдача призов
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
        DM.DisplayControlSentence(); //переключает на некст диалог
        DM.startQuest = false;
       
    }

    public void GetIdPrize()                     //метод получения ИД и коорд Х предмета перед его удалением со сцены
    {
        prizeId.Clear();
        prizeX.Clear();

        for (int i = 0; i < prize.Length; i++)
        {
            if (prize[i].GetComponent<Pickup>())        //проверка на предметы с подбором
            {
                prizeId.Add(prize[i].GetComponent<Pickup>().id);
                prizeX.Add(prize[i].transform.position.x);
            }
        }
    }
}