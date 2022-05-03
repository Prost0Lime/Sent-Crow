using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public Animator storyDisplayAnim;
    public StoryManager SM;

    public float timeActive;        //����� ������
    public bool permanent;
    public GameObject curentStory;
    public GameObject nextStory;

    public Story story;

    public GameObject[] itemStory; 

    public List<int> itemStoryId; 
    public List<float> itemStoryX; 

    public void Start()
    {
        storyDisplayAnim = GameObject.FindGameObjectWithTag("StoryD").GetComponent<Animator>();
    }

    public void GetIdItem()                     //����� ��������� �� � ����� � �������� ����� ��� ��������� �� �����
    {
        itemStoryId.Clear();
        itemStoryX.Clear();

        for (int i = 0; i < itemStory.Length; i++)
        {
            if (itemStory[i].GetComponent<Pickup>())        //�������� �� �������� � ��������
            {
                itemStoryId.Add(itemStory[i].GetComponent<Pickup>().id);
                itemStoryX.Add(itemStory[i].transform.position.x);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            storyDisplayAnim.SetBool("StoryOpen", true);
            SM.StartStroy(story);

            Invoke("TimeOff", timeActive);
        }
    }

    public void TimeOff()
    {
        storyDisplayAnim.SetBool("StoryOpen", false);
        
        if (permanent == false)
        {
            curentStory.SetActive(false);
           
            if (nextStory != null)
            {
                nextStory.SetActive(true);
            }
        }
        
        //��������� �������� ������ ����������� ����� �������
        for (int i=0; i < itemStory.Length; i++)
        {
            if (itemStory[i] != null)
            {
                itemStory[i].SetActive(true);
            }
        }
    }
}
