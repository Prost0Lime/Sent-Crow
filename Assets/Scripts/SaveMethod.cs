using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveMethod : MonoBehaviour
{
    public Inventory inventory;                     // ��������� ��������� Karl ��� IsFull
    public bool[] IsFullToSave;                     //������������� ������ � ��������� seril

    public ChestItemManager CIM;
    public int[] ChestItemIdToSave;             //���������

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

    public void Start()
    {
        if (mainmenuFade == null)           //��� ���������� ������ ��� ������� � ����
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            CIM = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestItemManager>();
            SMTS = GameObject.FindGameObjectWithTag("SMTS").GetComponent<SceneManagerToSave>();
        }
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

            bf.Serialize(file, data);
            file.Close();
            Debug.Log("���� ���������");
        }
    }
    public void SaveScene()    
    {
        if (SMTS.LastScene == 1)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/SentCrowScene1.stcw");
            SaveData data = new SaveData();

            idObjectToSave = SMTS.idObject;
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

            bf.Serialize(file, data);
            file.Close();
            Debug.Log("����� 1 ���������");
        }

        if (SMTS.LastScene == 2)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/SentCrowScene2.stcw");
            SaveData data = new SaveData();

            idObjectToSave = SMTS.idObject;
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

            bf.Serialize(file, data);
            file.Close();
            Debug.Log("����� 2 ���������");
        }
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

                else if (mainmenuFade == null)       //����� �� ����� �� ����� ������
                {
                    LoadPos();
                }
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

            //-------------------������ ��� �������� ��������� �� �����----------

            LoadScene();

            SMTS.LoadSavedObjects();

            Debug.Log("���� ���������");
        }
        else
            Debug.LogError("������ ���������!");
      
    }

    public void LoadScene()         //����� �������� �������� �����
    {
        if (SMTS.LastScene == 1)
        {
            if (File.Exists(Application.persistentDataPath + "/SentCrowScene1.stcw"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/SentCrowScene1.stcw", FileMode.Open);
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

                SMTS.LoadSavedObjects();
                Debug.Log("����� 1 ���������");
            }
        }
        if (SMTS.LastScene == 2)
        {
            if (File.Exists(Application.persistentDataPath + "/SentCrowScene2.stcw"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/SentCrowScene2.stcw", FileMode.Open);
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

                SMTS.LoadSavedObjects();
                Debug.Log("����� 2 ���������");
            }
        }
    }

    public void ResetData()                                             //�����
    {
        if (File.Exists(Application.persistentDataPath + "/SentCrow.stcw"))
        {
            File.Delete(Application.persistentDataPath + "/SentCrow.stcw");
            Debug.Log("�������� �����");
        }
        if (File.Exists(Application.persistentDataPath + "/SentCrowScene1.stcw"))
        {
            File.Delete(Application.persistentDataPath + "/SentCrowScene1.stcw");
        }
        if (File.Exists(Application.persistentDataPath + "/SentCrowScene2.stcw"))
        {
            File.Delete(Application.persistentDataPath + "/SentCrowScene2.stcw");
        }
        else
            Debug.LogError("���� ���������� ��� ��������");
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
}




