using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [HideInInspector]
    public SceneManagerToSave SMTS;
	public GameObject item;
    public int IdItemInventory;
    [HideInInspector]
    public Pickup pickup;

    [HideInInspector]
    private Transform player;
    [HideInInspector]
    private KarlController KC;
    public float distance = 0.5f;

    [HideInInspector]
    public LocationSpawnObjects LSO;

    private void Start()
    {
        pickup = item.GetComponent<Pickup>();
        SMTS = GameObject.FindGameObjectWithTag("SMTS").GetComponent<SceneManagerToSave>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        KC = GameObject.FindGameObjectWithTag("Player").GetComponent<KarlController>(); //для получения значений lookdirection
        LSO = GameObject.FindGameObjectWithTag("DropSpawn").GetComponent<LocationSpawnObjects>();
    }

    public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x + distance * KC.hor, player.position.y + distance * KC.ver);
        for (int i = 0; i < LSO.ParentLocation.Length; i++)         //спавн дропа только в определённых локациях для иерархии
        {
            if (LSO.ParentLocation[i].activeInHierarchy == true)
            {
                pickup.ItemScene = SceneManager.GetActiveScene().buildIndex;		//Ид сцены где находится объект перед сохранением для сохранения
                pickup.PickupIdSpawnLocation = i;       //присваивание ИД локации к объекту где он заспавнился
                Instantiate(item, playerPos, Quaternion.identity, LSO.ParentLocation[i].transform);
            }
        }
        if (pickup.id == 3) // провека, если это лампа, то переключить на анимации без ламп
        {
            KC.LampOff();
        }
    }   
}
