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

    public float[] PlayerXY;            //координаты игрока

    [HideInInspector]
    public LocationSpawnObjects LSO;

    public GameObject[] prefabObjectsToSpawn;       //массив предметов для загрузки
    private int[] idPrefabObjectsToSpawn;

    public List<GameObject> OBJ;        //лист объектов для сохранения
    public int[] idObject;              //Ид объекта
    public int[] idLocationForSpawn;     //ИД локации на которой нахоидится предмет
    public float[] ObjectPosX;       //массив координат Х объектов для сохранения
    public float[] ObjectPosY;       //массив координат Y объектов для сохранения
    public float[] ObjectPosZ;       //массив координат Z объектов для сохранения
    public bool[] ObjectSetActive;       //массив активированности объектов для сохранения

    public List<GameObject> itemStories;            //лист итемсторисов на сцене
    public bool[] itemStoriesActive;            //массив активности итемсторисов 

    public List<GameObject> NPC;            //лист нпс на сцене
    public bool[] NPCActive;                //массив активности нпс

    public int[] NPCSkipDialogue;       //количество реплик для скипа       
    public bool[] NPCStartQuest;        //квест запущен или нет
    public bool[] NPCContinueText;      //для остановки или продолжения диалога

    public int[] ItemsCount;        //колво предметов для удаления из кввеста при загрузке

    public float Volume;      //Громкость музыки

    void Start()
    {
        KC = GameObject.FindGameObjectWithTag("Player").GetComponent<KarlController>();
        LSO = GameObject.FindGameObjectWithTag("DropSpawn").GetComponent<LocationSpawnObjects>();

        idPrefabObjectsToSpawn = new int[prefabObjectsToSpawn.Length];
        for (int i = 0; i < idPrefabObjectsToSpawn.Length; i++)
        {
            idPrefabObjectsToSpawn[i] = prefabObjectsToSpawn[i].GetComponent<Pickup>().id;         //получение ИД от Префаба предмета и сохранение в новый массив 
        }

        LastScene = SceneManager.GetActiveScene().buildIndex;       //получение ИД активной сцены
        Invoke("Load", 1f);         //загрузка инвента объектов после загрузки сцены
    }

    void Load()
    {
        SM.LoadGame();          
    }

    public void SavePlayerPos()             //получение координат игрока для дальнейшего сохр
    {
        PlayerXY = new float[2] { 0, 0 };
        PlayerXY[0] = KC.transform.position.x;          //передаёт координаты в saveMethod для сохранения
        PlayerXY[1] = KC.transform.position.y;
    }

    public void SaveVolume()    //Сохранение громкости
    {
        Volume = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().volume;
    }

    public void LoadObjInList()             //получение всех объектов и добавление их в лист
    {
        OBJ.Clear();
        itemStories.Clear();
        NPC.Clear();

        GameObject[] arrayObj = Resources.FindObjectsOfTypeAll<GameObject>();
        for (int i = 0; i < arrayObj.Length; i++)
        {
            if (arrayObj[i].GetComponent<Pickup>())     //проверка на наличие скрипта на подбор 
            {
                if (arrayObj[i].transform.position.x != arrayObj[i].transform.position.y)     //отсеивание префабов не из иерархии
                {
                    OBJ.Add(arrayObj[i]);           //копирование из массива в лист
                }
            }
        
            if (arrayObj[i].GetComponent<StoryTrigger>())       //проверка на СториАйтем
            {
                if (arrayObj[i].transform.position.x != arrayObj[i].transform.position.y)     //отсеивание префабов не из иерархии
                {
                    itemStories.Add(arrayObj[i]);           //копирование из массива в лист
                }
            }

            if (arrayObj[i].GetComponent<DialogueTrigger>())        //проверка на NPC
            {
                if (arrayObj[i].transform.position.x != arrayObj[i].transform.position.y)
                {
                    NPC.Add(arrayObj[i]);
                }
            }
        }
    }

    public void SaveObjects()           //метод получения данных объектов для сохранения
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
            LSO.AllLocations[i].SetActive(true);    //включение всех локаций
        }

        for (int i = 0; i < LSO.AllLocations.Length; i++)
        {
            LSO.AllLocations[i].SetActive(true);    //включение всех локаций
        }

        for (int i = 0; i < OBJ.Count; i++)
        {

            idObject[i] = OBJ[i].GetComponent<Pickup>().id;     //получение ИД предмета 
            idLocationForSpawn[i] = OBJ[i].GetComponent<Pickup>().PickupIdSpawnLocation;     //получение Ид локации где лежал предмет             
            ObjectPosX[i] = OBJ[i].transform.position.x;        //получение координаты X
            ObjectPosY[i] = OBJ[i].transform.position.y;        //получение координаты Y
            ObjectPosZ[i] = OBJ[i].transform.position.z;        //получение координаты Z
            ObjectSetActive[i] = OBJ[i].activeInHierarchy;      //получение активности

        }

        itemStoriesActive = new bool[itemStories.Count];    
        
        for (int i = 0; i < itemStories.Count; i++)
        {
            itemStoriesActive[i] = itemStories[i].activeInHierarchy;        //получение активности сториАйтемов
        }

        NPCActive = new bool[NPC.Count];
        NPCSkipDialogue = new int[NPC.Count];
        NPCStartQuest = new bool[NPC.Count];
        NPCContinueText = new bool[NPC.Count];

        ItemsCount = new int[NPC.Count];    

        for (int i = 0; i < NPC.Count; i++)
        {
            NPCActive[i] = NPC[i].activeInHierarchy;                    //получение активности NPC
            NPCSkipDialogue[i] = NPC[i].GetComponent<DialogueTrigger>().numToDeleteQueue;   //получение колва диалогов для скипа
            NPCStartQuest[i] = NPC[i].GetComponent<DialogueTrigger>().startQuest;       //получение данных о старте квеста
            NPCContinueText[i] = NPC[i].GetComponent<DialogueTrigger>().continueText;   //получение данных об возможности продолжить диалог

            ItemsCount[i] = NPC[i].GetComponent<Quests>().itemsInStart - NPC[i].GetComponent<Quests>().items.Count;     //количество предметов общее - колво предметов на данный момент 

        }
    }

    public void LoadSavedObjects()      //загрузка объектов на сцену
    {       
        LoadObjInList();

        for (int i = 0; i < itemStories.Count; i++)         // загрузка ИД и коорд объектов в сториайтеме
        {
            itemStories[i].SetActive(true);
            itemStories[i].GetComponent<StoryTrigger>().GetIdItem();         
        }

        for (int i = 0; i < NPC.Count; i++)         // загрузка ИД и коорд объектов в НПС
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

            //---------------------отчистка сцены от всех предметов
        for (int i = 0; i < OBJ.Count; i++)         
        {
            DestroyImmediate(OBJ[i], true);
        }
       
        //---------------------спавн объектов на сцену
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

        LoadObjInList();    //перезагрузка листа объектов        
        for (int i = 0; i < idObject.Length; i++)
        {
            idObject[i] = OBJ[i].GetComponent<Pickup>().id;     //перезагрузка листа с id объекта 
            ObjectPosX[i] = OBJ[i].transform.position.x;        //перезагрузка листа с координатой X объекта
        }

        //--------------------предача сохранённых предметов из сцены в storyItem
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
            itemStories[t].SetActive(itemStoriesActive[t]);     //активность в соответствии с сохр          
        }

        //-------------------предача сохранённых предметов из сцены в NPC и данных квестов
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

            NPC[t].GetComponent<DialogueTrigger>().numToDeleteQueue = NPCSkipDialogue[t];   //присвоение данных по квесту
            NPC[t].GetComponent<DialogueTrigger>().startQuest = NPCStartQuest[t];
            NPC[t].GetComponent<DialogueTrigger>().continueText = NPCContinueText[t];

            NPC[t].SetActive(NPCActive[t]);     //активность npc в соответствии с сохр

            
        }
        
    }
    public void LoadSettingsSMTS() // метод загрузки насроек
    {
        GameObject.FindGameObjectWithTag("Audio").GetComponent<VolumeValue>().musicVolume = Volume;
    }
}
