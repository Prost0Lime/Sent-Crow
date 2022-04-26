using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	public GameObject chest;
	public ChestItemManager CIM;
	public Inventory inventory;
	public int i;

	private void Start()
	{
		
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		chest = GameObject.FindGameObjectWithTag("Chest"); // поиск рюкзака
		CIM = chest.GetComponent<ChestItemManager>();
	}

    private void Update()
    {
		if (transform.childCount <= 0)
		{
			inventory.isFull[i] = false;
		}
    }

	public void DropItem()
	{
		foreach (Transform child in transform)
		{
			child.GetComponent<Spawn>().SpawnDroppedItem();
			CIM.ChestItemId[i] = 0;
			GameObject.Destroy(child.gameObject);
			
		}
	}
}
