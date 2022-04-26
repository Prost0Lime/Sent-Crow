using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public SceneChanger SC;         //SceneChanger
    public Animator SwitchBttn;

    public int SceneToLoadB;        //сцена для загрузки
    public Vector3 positionB; //позиция на которую надо переместиться в новой сцене

    public GameObject ThisLocation; //текущая локация
    public GameObject[] otherLocations; // остальные локации сцены

    public bool BttnOnSwitchOn;    //включается только если игрок на триггере

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
