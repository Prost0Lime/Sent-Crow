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
        KC = GameObject.FindGameObjectWithTag("Player").GetComponent<KarlController>(); //��� ��������� �������� lookdirection
        LSO = GameObject.FindGameObjectWithTag("DropSpawn").GetComponent<LocationSpawnObjects>();
    }

    public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x + distance * KC.hor, player.position.y + distance * KC.ver);
        for (int i = 0; i < LSO.ParentLocation.Length; i++)         //����� ����� ������ � ����������� �������� ��� ��������
        {
            if (LSO.ParentLocation[i].activeInHierarchy == true)
            {
                pickup.ItemScene = SceneManager.GetActiveScene().buildIndex;		//�� ����� ��� ��������� ������ ����� ����������� ��� ����������
                pickup.PickupIdSpawnLocation = i;       //������������ �� ������� � ������� ��� �� �����������
                Instantiate(item, playerPos, Quaternion.identity, LSO.ParentLocation[i].transform);
            }
        }
        if (pickup.id == 3) // �������, ���� ��� �����, �� ����������� �� �������� ��� ����
        {
            KC.LampOff();
        }
    }   
}
