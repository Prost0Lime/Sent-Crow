using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemManager : MonoBehaviour
{

    private int[] IdItem;                       //������ � ���������� 
    public GameObject[] Items;                  //������ �� ���������

    public int[] ChestItemId;                   //������ ��������� � ���������
    [HideInInspector]
    public Inventory inventory;
    public void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        ChestItemId = new int[6] { 0, 0, 0, 0, 0, 0 };                      //������ ��������� � ���������

        IdItem = new int[Items.Length];
        for (int i=0; i < IdItem.Length; i++)
        {
            IdItem[i] = Items[i].GetComponent<Spawn>().IdItemInventory;         //��������� �� �� �������� � ���������� � ����� ������ 
        }
    }

    public void ReloadItem()     //����� ��� ���������� ��������� � ��������� ��� �������� �� ����
    {
        for (int i=0; i < ChestItemId.Length; i++)
        {
            for (int j = 0; j < IdItem.Length; j++)
            {
                if(ChestItemId[i] == IdItem[j])
                {
                    Instantiate(Items[j], inventory.slots[i].transform);
                }
            }
        }
    }
}