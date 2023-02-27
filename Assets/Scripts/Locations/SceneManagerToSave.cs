using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerToSave : MonoBehaviour
{
    public int LastScene;
    public SaveMethod SM;
    
    [HideInInspector]
    public KarlController KC;

    public float[] PlayerXY;            //���������� ������

    [HideInInspector]
    public LocationSpawnObjects LSO;

    public GameObject[] prefabObjectsToSpawn;       //������ ��������� ��� ��������
    private int[] idPrefabObjectsToSpawn;

    public List<GameObject> OBJ;        //���� �������� ��� ����������
    public int[] idObject;              //�� �������
    public int[] idLocationForSpawn;     //�� ������� �� ������� ���������� �������
    public float[] ObjectPosX;       //������ ��������� � �������� ��� ����������
    public float[] ObjectPosY;       //������ ��������� Y �������� ��� ����������
    public float[] ObjectPosZ;       //������ ��������� Z �������� ��� ����������
    public bool[] ObjectSetActive;       //������ ���������������� �������� ��� ����������

    public List<GameObject> itemStories;            //���� ������������ �� �����
    public bool[] itemStoriesActive;            //������ ���������� ������������ 

    public List<GameObject> NPC;            //���� ��� �� �����
    public bool[] NPCActive;                //������ ���������� ���

    public int[] NPCSkipDialogue;       //���������� ������ ��� �����       
    public bool[] NPCStartQuest;        //����� ������� ��� ���
    public bool[] NPCContinueText;      //��� ��������� ��� ����������� �������

    public int[] ItemsCount;        //����� ��������� ��� �������� �� ������� ��� ��������

    public float Volume;      //��������� ������

    void Start()
    {
        KC = GameObject.FindGameObjectWithTag("Player").GetComponent<KarlController>();
        LSO = GameObject.FindGameObjectWithTag("DropSpawn").GetComponent<LocationSpawnObjects>();

        idPrefabObjectsToSpawn = new int[prefabObjectsToSpawn.Length];
        for (int i = 0; i < idPrefabObjectsToSpawn.Length; i++)
        {
            idPrefabObjectsToSpawn[i] = prefabObjectsToSpawn[i].GetComponent<Pickup>().id;         //��������� �� �� ������� �������� � ���������� � ����� ������ 
        }

        LastScene = SceneManager.GetActiveScene().buildIndex;       //��������� �� �������� �����
        Invoke("Load", 1f);         //�������� ������� �������� ����� �������� �����
    }

    void Load()
    {
        SM.LoadGame();          
    }

    public void SavePlayerPos()             //��������� ��������� ������ ��� ����������� ����
    {
        PlayerXY = new float[2] { 0, 0 };
        PlayerXY[0] = KC.transform.position.x;          //������� ���������� � saveMethod ��� ����������
        PlayerXY[1] = KC.transform.position.y;
    }

    public void SaveVolume()    //���������� ���������
    {
        Volume = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().volume;
    }

    public void LoadObjInList()             //��������� ���� �������� � ���������� �� � ����
    {
        OBJ.Clear();
        itemStories.Clear();
        NPC.Clear();

        GameObject[] arrayObj = Resources.FindObjectsOfTypeAll<GameObject>();
        for (int i = 0; i < arrayObj.Length; i++)
        {
            if (arrayObj[i].GetComponent<Pickup>())     //�������� �� ������� ������� �� ������ 
            {
                if (arrayObj[i].transform.position.x != arrayObj[i].transform.position.y)     //���������� �������� �� �� ��������
                {
                    OBJ.Add(arrayObj[i]);           //����������� �� ������� � ����
                }
            }
        
            if (arrayObj[i].GetComponent<StoryTrigger>())       //�������� �� ����������
            {
                if (arrayObj[i].transform.position.x != arrayObj[i].transform.position.y)     //���������� �������� �� �� ��������
                {
                    itemStories.Add(arrayObj[i]);           //����������� �� ������� � ����
                }
            }

            if (arrayObj[i].GetComponent<DialogueTrigger>())        //�������� �� NPC
            {
                if (arrayObj[i].transform.position.x != arrayObj[i].transform.position.y)
                {
                    NPC.Add(arrayObj[i]);
                }
            }
        }
    }

    public void SaveObjects()           //����� ��������� ������ �������� ��� ����������
    {

        LoadObjInList();
        
        ObjectPosX = new float[OBJ.Count];
        ObjectPosY = new float[OBJ.Count];
        ObjectPosZ = new float[OBJ.Count];
        idObject = new int[OBJ.Count];
        idLocationForSpawn = new int[OBJ.Count];
        ObjectSetActive = new bool[OBJ.Count];
        
        for (int i = 0; i < LSO.AllLocations.Length; i++)
        {
            LSO.AllLocations[i].SetActive(true);    //��������� ���� �������
        }

        for (int i = 0; i < LSO.AllLocations.Length; i++)
        {
            LSO.AllLocations[i].SetActive(true);    //��������� ���� �������
        }

        for (int i = 0; i < OBJ.Count; i++)
        {

            idObject[i] = OBJ[i].GetComponent<Pickup>().id;     //��������� �� �������� 
            idLocationForSpawn[i] = OBJ[i].GetComponent<Pickup>().PickupIdSpawnLocation;     //��������� �� ������� ��� ����� �������             
            ObjectPosX[i] = OBJ[i].transform.position.x;        //��������� ���������� X
            ObjectPosY[i] = OBJ[i].transform.position.y;        //��������� ���������� Y
            ObjectPosZ[i] = OBJ[i].transform.position.z;        //��������� ���������� Z
            ObjectSetActive[i] = OBJ[i].activeInHierarchy;      //��������� ����������

        }

        itemStoriesActive = new bool[itemStories.Count];    
        
        for (int i = 0; i < itemStories.Count; i++)
        {
            itemStoriesActive[i] = itemStories[i].activeInHierarchy;        //��������� ���������� ������������
        }

        NPCActive = new bool[NPC.Count];
        NPCSkipDialogue = new int[NPC.Count];
        NPCStartQuest = new bool[NPC.Count];
        NPCContinueText = new bool[NPC.Count];

        ItemsCount = new int[NPC.Count];    

        for (int i = 0; i < NPC.Count; i++)
        {
            NPCActive[i] = NPC[i].activeInHierarchy;                    //��������� ���������� NPC
            NPCSkipDialogue[i] = NPC[i].GetComponent<DialogueTrigger>().numToDeleteQueue;   //��������� ����� �������� ��� �����
            NPCStartQuest[i] = NPC[i].GetComponent<DialogueTrigger>().startQuest;       //��������� ������ � ������ ������
            NPCContinueText[i] = NPC[i].GetComponent<DialogueTrigger>().continueText;   //��������� ������ �� ����������� ���������� ������

            ItemsCount[i] = NPC[i].GetComponent<Quests>().itemsInStart - NPC[i].GetComponent<Quests>().items.Count;     //���������� ��������� ����� - ����� ��������� �� ������ ������ 

        }
    }

    public void LoadSavedObjects()      //�������� �������� �� �����
    {       
        LoadObjInList();

        for (int i = 0; i < itemStories.Count; i++)         // �������� �� � ����� �������� � �����������
        {
            itemStories[i].SetActive(true);
            itemStories[i].GetComponent<StoryTrigger>().GetIdItem();         
        }

        for (int i = 0; i < NPC.Count; i++)         // �������� �� � ����� �������� � ���
        {
            NPC[i].SetActive(true);
            NPC[i].GetComponent<Quests>().GetIdPrize();
        
            if (ItemsCount[i] != 0)
            {
                for (int x = 0; x < ItemsCount[i]; x++)
                {
                    NPC[i].GetComponent<Quests>().items.RemoveAt(0);
                    Debug.Log("delete");
                  
                }
            }
        }

            //---------------------�������� ����� �� ���� ���������
        for (int i = 0; i < OBJ.Count; i++)         
        {
            DestroyImmediate(OBJ[i], true);
        }
       
        //---------------------����� �������� �� �����
        for (int i = 0; i < idObject.Length; i++)
        {
            for (int j = 0; j < idPrefabObjectsToSpawn.Length; j++)
            {
                if (idObject[i] == idPrefabObjectsToSpawn[j]) 
                {
                    prefabObjectsToSpawn[j].GetComponent<Pickup>().PickupIdSpawnLocation = idLocationForSpawn[i];
                    Instantiate(prefabObjectsToSpawn[j], new Vector3(ObjectPosX[i], ObjectPosY[i], ObjectPosZ[i]), Quaternion.identity,
                        LSO.ParentLocation[(idLocationForSpawn[i])].transform).SetActive(ObjectSetActive[i]);

                }
            }
        }

        LoadObjInList();    //������������ ����� ��������        
        for (int i = 0; i < idObject.Length; i++)
        {
            idObject[i] = OBJ[i].GetComponent<Pickup>().id;     //������������ ����� � id ������� 
            ObjectPosX[i] = OBJ[i].transform.position.x;        //������������ ����� � ����������� X �������
        }

        //--------------------������� ���������� ��������� �� ����� � storyItem
        for (int t = 0; t < itemStories.Count; t++)
        {
            if (itemStories[t].GetComponent<StoryTrigger>().itemStoryId.Count != 0)
            {
                for (int i = 0; i < itemStories[t].GetComponent<StoryTrigger>().itemStoryId.Count; i++)
                {
                    for (int j = 0; j < idObject.Length; j++)
                    {
                        if ((itemStories[t].GetComponent<StoryTrigger>().itemStoryId[i] == idObject[j])
                            && (itemStories[t].GetComponent<StoryTrigger>().itemStoryX[i] == ObjectPosX[j]))
                        {
                            itemStories[t].GetComponent<StoryTrigger>().itemStory[i] = OBJ[j];
                        }
                    }
                }
            }
            itemStories[t].SetActive(itemStoriesActive[t]);     //���������� � ������������ � ����          
        }

        //-------------------������� ���������� ��������� �� ����� � NPC � ������ �������
        for (int t = 0; t < NPC.Count; t++)
        {
            if (NPC[t].GetComponent<Quests>().prizeId.Count != 0)
            {
                for (int i = 0; i < NPC[t].GetComponent<Quests>().prizeId.Count; i++)
                {
                    for (int j = 0; j < idObject.Length; j++)
                    {
                        if ((NPC[t].GetComponent<Quests>().prizeId[i] == idObject[j])
                            && (NPC[t].GetComponent<Quests>().prizeX[i] == ObjectPosX[j]))
                        {
                            NPC[t].GetComponent<Quests>().prize[i] = OBJ[j];
                        }
                    }
                }
            }

            NPC[t].GetComponent<DialogueTrigger>().numToDeleteQueue = NPCSkipDialogue[t];   //���������� ������ �� ������
            NPC[t].GetComponent<DialogueTrigger>().startQuest = NPCStartQuest[t];
            NPC[t].GetComponent<DialogueTrigger>().continueText = NPCContinueText[t];

            NPC[t].SetActive(NPCActive[t]);     //���������� npc � ������������ � ����

            
        }
        
    }
    public void LoadSettingsSMTS() // ����� �������� �������
    {
        GameObject.FindGameObjectWithTag("Audio").GetComponent<VolumeValue>().musicVolume = Volume;
    }
}
