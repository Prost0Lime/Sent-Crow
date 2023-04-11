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
    public Inventory inventory;                     // ��������� ��������� Karl ��� IsFull
    public bool[] IsFullToSave;                     //������������� ������ � ��������� seril

    [HideInInspector]
    public ChestItemManager CIM;
    public int[] ChestItemIdToSave;             //���������

    [HideInInspector]
    public SceneManagerToSave SMTS;             

    public MainMenu mainmenuFade;           //������ ��� �������� �� ����
    public int LastSceneToSave;             //����� ���� �����

    public VectorValue playerStorage;       //���������� ��������� ������
    public float[] PlayerXYToSave;

    public int[] idObjectToSave;        //�� �������� ��� ����������
    public int[] idObjectLocationToSave;        //�� ������� ��� ��� �������
    
    public float[] ObjectPosXToSave;    //��� ������ 
    public float[] ObjectPosYToSave;    
    public float[] ObjectPosZToSave;    
    public bool[] ObjectSetActiveToSave;    //��� ���������� � �����

    public bool[] itemStoryActiveToSave;        //���������� ������������

    public bool[] NPCActiveToSave;      //���������� NPC

    public bool[] TeleportActiveToSave;      //���������� ����������

    public int[] NPCSkipDialogueToSave; //������ ������� � ������
    public bool[] NPCStartQuestToSave;
    public bool[] NPCContinueTextToSave;
    public int[] QuestItemToSave;

    public float VolumeToSave;    //��������� ������

    public bool LoadComplite;       //���������� ��� ���������� fade ����� ��������

    public void Start()
    {
        if (mainmenuFade == null)           //��� ���������� ������ ��� ������� � ����
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            CIM = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestItemManager>();
        }
        SMTS = GameObject.FindGameObjectWithTag("SMTS").GetComponent<SceneManagerToSave>();
        LoadComplite = false;
    }

    public void SaveGame()                                              //���������� ������
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SentCrow.stcw");
        SaveData data = new SaveData();

        //--------------------������ � ��������� ����� �� ������� ��� �����---------------
        LastSceneToSave = SMTS.LastScene;
        data.savedLastScene = LastSceneToSave;

        if (SceneManager.GetActiveScene().buildIndex != 0)  //-------------���������� ���������� �� ����� ����
        {
            //--------------------������ ��� ���������� ������������� ��������� IsFull---------------        
            IsFullToSave = inventory.isFull;
            data.savedIsFull = IsFullToSave;

            //--------------------�������� ��� ���������� ��������� ---------------           
            ChestItemIdToSave = CIM.ChestItemId;
            data.savedChestItemId = ChestItemIdToSave;

            //---------------------��� ���������� ����������� ������------------------
            SMTS.SavePlayerPos();
            PlayerXYToSave = SMTS.PlayerXY;
            playerStorage.initialValue.x = PlayerXYToSave[0];        //�������� ������  
            playerStorage.initialValue.y = PlayerXYToSave[1];       //^
            data.savedPlayerXY = PlayerXYToSave;

            //--------------------��� ���������� ��������� �� ������ ������---------
            SMTS.SaveObjects();

            SaveScene();
        }

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("���� ���������");
    }
    public void SaveScene()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SentCrowScene" + SMTS.LastScene + ".stcw");
        SaveData data = new SaveData();

        idObjectToSave = SMTS.idObject;             //���������� ������ �������� �����
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

        itemStoryActiveToSave = SMTS.itemStoriesActive;         //���������� ���������� ������������
        data.savedItemStoryActive = itemStoryActiveToSave;

        NPCActiveToSave = SMTS.NPCActive;                   //���������� ���������� NPC
        data.savedNPCActive = NPCActiveToSave;

        TeleportActiveToSave = SMTS.teleportsActive;                   //���������� ���������� NPC
        data.savedTeleportActive = TeleportActiveToSave;

        NPCSkipDialogueToSave = SMTS.NPCSkipDialogue;   //���������� ���������� � ��������� ������ 
        NPCStartQuestToSave = SMTS.NPCStartQuest;
        NPCContinueTextToSave = SMTS.NPCContinueText;
        data.savedNPCSkipDialogue = NPCSkipDialogueToSave;
        data.savedNPCStartQuest = NPCStartQuestToSave;
        data.savedNPCContinueText = NPCContinueTextToSave;
        QuestItemToSave = SMTS.ItemsCount;
        data.savedQuestItem = QuestItemToSave;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("����� ���������");

        SaveSettings();
    }

    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SentCrowSettings.stcw");
        SaveData data = new SaveData();

        //--------------------��� ���������� ��������� �� ������ ������---------
        SMTS.SaveVolume();
        VolumeToSave = SMTS.Volume;
        data.savedVolume = VolumeToSave;

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("��������� ���������");

    }

    public void LoadGame()                                              //��������
    {
        if (File.Exists(Application.persistentDataPath + "/SentCrow.stcw"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SentCrow.stcw", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            //--------------------������� �� ��������� ����� ��� ��� �����---------------
            LastSceneToSave = data.savedLastScene;

            if (SceneManager.GetActiveScene().buildIndex != LastSceneToSave)        //��������� ������� ����� �� ������ ����������
            {
                if (mainmenuFade != null)       //�������� ����������� ������� �� ����
                {
                    mainmenuFade.SceneMenu = LastSceneToSave;
                    LoadPos();
                    mainmenuFade.LoadGame();

                }

                /*                else if (mainmenuFade == null)       //����� �� ����� �� ����� ������
                                {
                                    LoadPos();
                                }*/
            }

            void LoadPos()  //��������� ����� ��� �������� ��������� ��� ����� �����
            {
                //-------------------������ ��� ����������� ������ �� ���������� ����������----------------
                PlayerXYToSave = data.savedPlayerXY;
                playerStorage.initialValue.x = PlayerXYToSave[0];
                playerStorage.initialValue.y = PlayerXYToSave[1];
            }

            //--------------------������ ��� �������� ������������� ��������� IsFull---------------
            IsFullToSave = data.savedIsFull;
            inventory.isFull = IsFullToSave;

            //--------------------�������� ��� ���������� ��������� ---------------
            ChestItemIdToSave = data.savedChestItemId;
            CIM.ChestItemId = ChestItemIdToSave;
            CIM.ReloadItem();
            
            LoadScene();        //����� �������� �� ������
            LoadSettings();     //����� �������� ��������

            LoadComplite = true;
            Debug.Log("���� ���������");
        }
        else
        {
            LoadComplite = true;
            Debug.LogError("������ ���������!");
        }
            

    }

    public void LoadScene()         //����� �������� �������� �����
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

            itemStoryActiveToSave = data.savedItemStoryActive;         //�������� ���������� ������������
            SMTS.itemStoriesActive = itemStoryActiveToSave;

            NPCActiveToSave = data.savedNPCActive;       //�������� ���������� NPC
            SMTS.NPCActive = NPCActiveToSave;

            TeleportActiveToSave = data.savedTeleportActive;       //�������� ���������� ����������
            SMTS.teleportsActive = TeleportActiveToSave;

            NPCSkipDialogueToSave = data.savedNPCSkipDialogue;  //�������� ���������� � ��������� ������ 
            NPCStartQuestToSave = data.savedNPCStartQuest;
            NPCContinueTextToSave = data.savedNPCContinueText;
            SMTS.NPCSkipDialogue = NPCSkipDialogueToSave;
            SMTS.NPCStartQuest = NPCStartQuestToSave;
            SMTS.NPCContinueText = NPCContinueTextToSave;

            QuestItemToSave = data.savedQuestItem;
            SMTS.ItemsCount = QuestItemToSave;

            SMTS.LoadSavedObjects();
            Debug.Log("����� ���������");
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
            
            //--------------------��������� �������� ���������---------------
            VolumeToSave = data.savedVolume;
            SMTS.Volume = VolumeToSave;
            SMTS.LoadSettingsSMTS();

            Debug.Log("��������� ���������");
        }
    }

    public void ResetData()                                             //�����
    {
        if (Directory.GetFiles(Application.persistentDataPath).Length == 0)
        {
            Debug.LogError("���� ���������� ��� ��������");
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
    public int savedLastScene;          //������ �� ����� ����� ��� �����
    public bool[] savedIsFull;        //������������� ������ � ��������� 
    public int[] savedChestItemId;      //�������� � ��������� 
    public float[] savedPlayerXY;       //������� ������ 

    public int[] savedIdObject;        //���������� �� ������� 
    public int[] savedIdObjectLocation;        //���������� �� ������� ��� ��� �������
    public float[] savedObjectPosX;    //��� ������ 
    public float[] savedObjectPosY;
    public float[] savedObjectPosZ;
    public bool[] savedObjectSetActive;    //��� ���������� � �����

    public bool[] savedItemStoryActive;     //���������� StoryItem

    public bool[] savedNPCActive;      //���������� NPC

    public bool[] savedTeleportActive;      //���������� ����������

    public int[] savedNPCSkipDialogue;  //����� �������� ��� �����
    public bool[] savedNPCStartQuest;   //����� ������� ��� ������ ������
    public bool[] savedNPCContinueText; //������ ��� ����������� �������
    public int[] savedQuestItem;        //�������� ����������� ��� ������

    public float savedVolume; 

}




