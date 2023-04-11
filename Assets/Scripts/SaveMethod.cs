using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveMethod : MonoBehaviour
{
    [HideInInspector]
    public Inventory inventory;                     // получения инвентаря Karl для IsFull
    public bool[] IsFullToSave;                     //заполненность слотов в инвентаре seril

    [HideInInspector]
    public ChestItemManager CIM;
    public int[] ChestItemIdToSave;             //инвентарь

    [HideInInspector]
    public SceneManagerToSave SMTS;             

    public MainMenu mainmenuFade;           //скрипт для загрузки из меню
    public int LastSceneToSave;             //номер ласт сцены

    public VectorValue playerStorage;       //сохранение координат игрока
    public float[] PlayerXYToSave;

    public int[] idObjectToSave;        //Ид предмета для сохранения
    public int[] idObjectLocationToSave;        //Ид локации где был предмет
    
    public float[] ObjectPosXToSave;    //его коорды 
    public float[] ObjectPosYToSave;    
    public float[] ObjectPosZToSave;    
    public bool[] ObjectSetActiveToSave;    //его активность в сцене

    public bool[] itemStoryActiveToSave;        //активность сториАйтемов

    public bool[] NPCActiveToSave;      //активность NPC

    public bool[] TeleportActiveToSave;      //активность телепортов

    public int[] NPCSkipDialogueToSave; //данные диалога и квеста
    public bool[] NPCStartQuestToSave;
    public bool[] NPCContinueTextToSave;
    public int[] QuestItemToSave;

    public float VolumeToSave;    //Громкость музыки

    public bool LoadComplite;       //переменная для выключения fade после загрузки

    public void Start()
    {
        if (mainmenuFade == null)           //для устранения ошибок при запуске в меню
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            CIM = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestItemManager>();
        }
        SMTS = GameObject.FindGameObjectWithTag("SMTS").GetComponent<SceneManagerToSave>();
        LoadComplite = false;
    }

    public void SaveGame()                                              //Сохранение данных
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SentCrow.stcw");
        SaveData data = new SaveData();

        //--------------------данные о последней сцене на которой был игрок---------------
        LastSceneToSave = SMTS.LastScene;
        data.savedLastScene = LastSceneToSave;

        if (SceneManager.GetActiveScene().buildIndex != 0)  //-------------запрещение сохранение на сцене Меню
        {
            //--------------------данные для сохранения заполненности инвентаря IsFull---------------        
            IsFullToSave = inventory.isFull;
            data.savedIsFull = IsFullToSave;

            //--------------------предметы для сохранения инвентаря ---------------           
            ChestItemIdToSave = CIM.ChestItemId;
            data.savedChestItemId = ChestItemIdToSave;

            //---------------------для сохранения координатов игрока------------------
            SMTS.SavePlayerPos();
            PlayerXYToSave = SMTS.PlayerXY;
            playerStorage.initialValue.x = PlayerXYToSave[0];        //возможно лишнее  
            playerStorage.initialValue.y = PlayerXYToSave[1];       //^
            data.savedPlayerXY = PlayerXYToSave;

            //--------------------для сохранения предметов на разных сценах---------
            SMTS.SaveObjects();

            SaveScene();
        }

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Игра сохранена");
    }
    public void SaveScene()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SentCrowScene" + SMTS.LastScene + ".stcw");
        SaveData data = new SaveData();

        idObjectToSave = SMTS.idObject;             //сохранение данных объектов пикап
        idObjectLocationToSave = SMTS.idLocationForSpawn;
        ObjectPosXToSave = SMTS.ObjectPosX;
        ObjectPosYToSave = SMTS.ObjectPosY;
        ObjectPosZToSave = SMTS.ObjectPosZ;
        ObjectSetActiveToSave = SMTS.ObjectSetActive;
        data.savedIdObject = idObjectToSave;
        data.savedIdObjectLocation = idObjectLocationToSave;
        data.savedObjectPosX = ObjectPosXToSave;
        data.savedObjectPosY = ObjectPosYToSave;
        data.savedObjectPosZ = ObjectPosZToSave;
        data.savedObjectSetActive = ObjectSetActiveToSave;

        itemStoryActiveToSave = SMTS.itemStoriesActive;         //сохранение активности сториАйтемов
        data.savedItemStoryActive = itemStoryActiveToSave;

        NPCActiveToSave = SMTS.NPCActive;                   //сохранение активности NPC
        data.savedNPCActive = NPCActiveToSave;

        TeleportActiveToSave = SMTS.teleportsActive;                   //сохранение активности NPC
        data.savedTeleportActive = TeleportActiveToSave;

        NPCSkipDialogueToSave = SMTS.NPCSkipDialogue;   //сохранение диалоговых и квестовых данных 
        NPCStartQuestToSave = SMTS.NPCStartQuest;
        NPCContinueTextToSave = SMTS.NPCContinueText;
        data.savedNPCSkipDialogue = NPCSkipDialogueToSave;
        data.savedNPCStartQuest = NPCStartQuestToSave;
        data.savedNPCContinueText = NPCContinueTextToSave;
        QuestItemToSave = SMTS.ItemsCount;
        data.savedQuestItem = QuestItemToSave;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Сцена сохранена");

        SaveSettings();
    }

    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SentCrowSettings.stcw");
        SaveData data = new SaveData();

        //--------------------для сохранения громкости на разных сценах---------
        SMTS.SaveVolume();
        VolumeToSave = SMTS.Volume;
        data.savedVolume = VolumeToSave;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Настройки сохранены");

    }

    public void LoadGame()                                              //загрузка
    {
        if (File.Exists(Application.persistentDataPath + "/SentCrow.stcw"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SentCrow.stcw", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            //--------------------переход на последнюю сцену где был игрок---------------
            LastSceneToSave = data.savedLastScene;

            if (SceneManager.GetActiveScene().buildIndex != LastSceneToSave)        //сравнение текущей сцены со сценой сохранения
            {
                if (mainmenuFade != null)       //проверка переходится переход из меню
                {
                    mainmenuFade.SceneMenu = LastSceneToSave;
                    LoadPos();
                    mainmenuFade.LoadGame();

                }

                /*                else if (mainmenuFade == null)       //особо не думал но вроде лишнее
                                {
                                    LoadPos();
                                }*/
            }

            void LoadPos()  //локальный метод для загрузки координат для смены сцены
            {
                //-------------------данные для перемещения игрока на сохранённые координаты----------------
                PlayerXYToSave = data.savedPlayerXY;
                playerStorage.initialValue.x = PlayerXYToSave[0];
                playerStorage.initialValue.y = PlayerXYToSave[1];
            }

            //--------------------данные для загрузки заполненности инвентаря IsFull---------------
            IsFullToSave = data.savedIsFull;
            inventory.isFull = IsFullToSave;

            //--------------------предметы для сохранения инвентаря ---------------
            ChestItemIdToSave = data.savedChestItemId;
            CIM.ChestItemId = ChestItemIdToSave;
            CIM.ReloadItem();
            
            LoadScene();        //метод загрузки по сценам
            LoadSettings();     //метод загрузки настроек

            LoadComplite = true;
            Debug.Log("Игра загружена");
        }
        else
        {
            LoadComplite = true;
            Debug.LogError("Нечего загружать!");
        }
            

    }

    public void LoadScene()         //метод загрузки объектов сцены
    {
        if (File.Exists(Application.persistentDataPath + "/SentCrowScene" + SMTS.LastScene + ".stcw"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SentCrowScene" + SMTS.LastScene + ".stcw", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            idObjectToSave = data.savedIdObject;
            idObjectLocationToSave = data.savedIdObjectLocation;
            ObjectPosXToSave = data.savedObjectPosX;
            ObjectPosYToSave = data.savedObjectPosY;
            ObjectPosZToSave = data.savedObjectPosZ;
            ObjectSetActiveToSave = data.savedObjectSetActive;

            SMTS.idObject = idObjectToSave;
            SMTS.idLocationForSpawn = idObjectLocationToSave;
            SMTS.ObjectPosX = ObjectPosXToSave;
            SMTS.ObjectPosY = ObjectPosYToSave;
            SMTS.ObjectPosZ = ObjectPosZToSave;
            SMTS.ObjectSetActive = ObjectSetActiveToSave;

            itemStoryActiveToSave = data.savedItemStoryActive;         //загрузка активности сториАйтемов
            SMTS.itemStoriesActive = itemStoryActiveToSave;

            NPCActiveToSave = data.savedNPCActive;       //загрузка активности NPC
            SMTS.NPCActive = NPCActiveToSave;

            TeleportActiveToSave = data.savedTeleportActive;       //загрузка активности телепортов
            SMTS.teleportsActive = TeleportActiveToSave;

            NPCSkipDialogueToSave = data.savedNPCSkipDialogue;  //загрузка диалоговых и квестовых данных 
            NPCStartQuestToSave = data.savedNPCStartQuest;
            NPCContinueTextToSave = data.savedNPCContinueText;
            SMTS.NPCSkipDialogue = NPCSkipDialogueToSave;
            SMTS.NPCStartQuest = NPCStartQuestToSave;
            SMTS.NPCContinueText = NPCContinueTextToSave;

            QuestItemToSave = data.savedQuestItem;
            SMTS.ItemsCount = QuestItemToSave;

            SMTS.LoadSavedObjects();
            Debug.Log("Сцена загружена");
        }

    }

    public void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/SentCrowSettings.stcw"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SentCrowSettings.stcw", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            
            //--------------------Параметры настроек Громкость---------------
            VolumeToSave = data.savedVolume;
            SMTS.Volume = VolumeToSave;
            SMTS.LoadSettingsSMTS();

            Debug.Log("Настройки загружены");
        }
    }

    public void ResetData()                                             //сброс
    {
        if (Directory.GetFiles(Application.persistentDataPath).Length == 0)
        {
            Debug.LogError("Нету сохранений для удаления");
        }

        foreach (string filename in Directory.GetFiles(Application.persistentDataPath))
        {
            File.Delete(filename);
        }
    }
}

[Serializable]
class SaveData
{
    public int savedLastScene;          //данные на какой сцене был игрок
    public bool[] savedIsFull;        //заполненность слотов в инвентаре 
    public int[] savedChestItemId;      //предметы в инвентаре 
    public float[] savedPlayerXY;       //позиция игрока 

    public int[] savedIdObject;        //сохранённый Ид объекта 
    public int[] savedIdObjectLocation;        //сохранённый Ид локации где был предмет
    public float[] savedObjectPosX;    //его коорды 
    public float[] savedObjectPosY;
    public float[] savedObjectPosZ;
    public bool[] savedObjectSetActive;    //его активность в сцене

    public bool[] savedItemStoryActive;     //активность StoryItem

    public bool[] savedNPCActive;      //активность NPC

    public bool[] savedTeleportActive;      //активность телепортов

    public int[] savedNPCSkipDialogue;  //колво диалогов для скипа
    public bool[] savedNPCStartQuest;   //номер диалога для старта квеста
    public bool[] savedNPCContinueText; //статус для продолжения диалога
    public int[] savedQuestItem;        //предметы необходимые для квеста

    public float savedVolume; 

}




