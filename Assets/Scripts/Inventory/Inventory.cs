using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;
    public bool inventoryOn;

    [HideInInspector]
    public SoundManager soundManager;

    float timerDisplay;
    public float displayTime = 4.0f;

    public Animator invent;

    private void Start()
    {
        inventoryOn = false;
        inventory.SetActive(false);
        timerDisplay = -1.0f;
        soundManager = GameObject.FindGameObjectWithTag("AM").GetComponent<SoundManager>();
    }
    void Update()       //автоскрытие инвентаря
    {
        if (timerDisplay >= 0) 
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                invent.SetBool("openInventory", false);
                inventoryOn = false;
                Invoke("ChestActive", 0.2f);
            }
        }
    }

    public void Chest()
    {
        if (inventoryOn == false)
        {
            inventoryOn = true;
            inventory.SetActive(true);
            invent.SetBool("openInventory", true);
            timerDisplay = displayTime;
            soundManager.AudioSound2.clip = soundManager.Chest;
            soundManager.AudioSound2.Play();

        }
        else if (inventoryOn == true)
        {
            invent.SetBool("openInventory", false);
            inventoryOn = false;
            Invoke("ChestActive", 0.2f);
        }
    }

    public void ChestActive()
    {
        inventory.SetActive(false);
    }
}
