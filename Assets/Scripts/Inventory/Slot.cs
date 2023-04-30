using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	[HideInInspector]
	public GameObject chest;
	[HideInInspector]
	public ChestItemManager CIM;
	[HideInInspector]
	public Inventory inventory;
	public int i;
    public SoundManager soundManager;

    private void Start()
	{
        soundManager = GameObject.FindGameObjectWithTag("AM").GetComponent<SoundManager>();
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
            soundManager.AudioSound2.clip = soundManager.Drop;
            soundManager.AudioSound2.Play();

        }
	}
}
