using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public SceneChanger SC;         //SceneChanger
    public Animator SwitchBttn;

    public int SceneToLoadB;        //����� ��� ��������
    public Vector3 positionB; //������� �� ������� ���� ������������� � ����� �����

    public GameObject ThisLocation; //������� �������
    public GameObject[] otherLocations; // ��������� ������� �����

    public bool BttnOnSwitchOn;    //���������� ������ ���� ����� �� ��������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BttnOnSwitchOn = true;
            SwitchBttn.SetTrigger("isTriggered");
            ThisLocation.SetActive(true);
           
            foreach (GameObject location in otherLocations)
            {
                location.SetActive(false);
            }

            SC.sceneToLoad = SceneToLoadB;
            SC.position = positionB;
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BttnOnSwitchOn = false;
            SwitchBttn.SetTrigger("isTriggered");
        }
    }
}
