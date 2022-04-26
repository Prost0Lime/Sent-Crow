using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{ 
    public GameObject DoorPrefab;
    public Animator doorBttnAnim;
    public DoorManager DoorM;
    public bool DoorIsOpen; // ������ �� ��������� (������� ��� �������)
    private bool YouStayOnTrigger; //��� �������� ����� � ������� ������

    public void Update() //������������ ��������� ������� ����� �� ���
    {
        if ((DoorM.triggerOpen == true) && (YouStayOnTrigger == true))  
        {
            DoorPrefab.GetComponent<DoorTrigger>().DoorIsOpen = true;
            DoorPrefab.GetComponent<BoxCollider2D>().enabled = false;
            DoorPrefab.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        YouStayOnTrigger = true;
        
        if ((other.tag == "Player") && (DoorIsOpen == false))
        {
            doorBttnAnim.SetBool("BttnOn", true);
        }

        else if ((other.tag == "Player") && (DoorIsOpen == true))
        {
            DoorPrefab.GetComponent<BoxCollider2D>().enabled = false;
            DoorPrefab.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    

    public void OnTriggerExit2D(Collider2D other)
    {
        YouStayOnTrigger = false;

        if (other.tag == "Player")
        {
            doorBttnAnim.SetBool("BttnOn", false);
        }
        
        if(DoorIsOpen == true)
        {
            DoorPrefab.GetComponent<BoxCollider2D>().enabled = true;
            DoorPrefab.GetComponent<SpriteRenderer>().enabled = true;
        }
    }  
}


