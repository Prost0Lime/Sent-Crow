using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Pickup : MonoBehaviour
{
	public Inventory inventory;
	public Spawn spawn;
    public GameObject slotButton;
    public GameObject chest;
	public ChestItemManager CIM;

	Animator chestAnim;
	public int id;
	public int PickupIdSpawnLocation;	//Ид локации на которой он находится
	public int ItemScene;               //Ид сцены где находится объект перед сохранением для сохранения

	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		chest = GameObject.FindGameObjectWithTag("Chest"); // поиск рюкзака
		chestAnim = chest.GetComponent<Animator>(); // получение аниматора
		spawn = slotButton.GetComponent<Spawn>();
		CIM = chest.GetComponent<ChestItemManager>();
		
	}

    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{ 
			for (int i = 0; i < inventory.slots.Length; i++)
			{
				if (inventory.isFull[i] == false)
				{
					inventory.isFull[i] = true;
					Instantiate(slotButton, inventory.slots[i].transform);		//добавляет предмет

						if (CIM.ChestItemId[0] == 0)									//присваивание ИД объекта в массив рюкзака
						{
							CIM.ChestItemId[0] = spawn.IdItemInventory;
						}
						else if (CIM.ChestItemId[1] == 0)

						{
							CIM.ChestItemId[1] = spawn.IdItemInventory;
						}
						else if (CIM.ChestItemId[2] == 0)

						{
							CIM.ChestItemId[2] = spawn.IdItemInventory;
						}
						else if (CIM.ChestItemId[3] == 0)

						{
							CIM.ChestItemId[3] = spawn.IdItemInventory;
						}
						else if (CIM.ChestItemId[4] == 0)

						{
							CIM.ChestItemId[4] = spawn.IdItemInventory;
						}
						else if (CIM.ChestItemId[5] == 0)

						{
							CIM.ChestItemId[5] = spawn.IdItemInventory;
						}				
					Destroy(gameObject);
					chestAnim.SetTrigger("Pickup"); //запуск анимации рюкзака
					break;
				}
			}
		}
	}

} 
