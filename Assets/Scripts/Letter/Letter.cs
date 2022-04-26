using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public KarlController karl;
    //public Inventory inven;
    public DisplayControl displayControl;

    
    void Start()
    {
        karl = GameObject.FindGameObjectWithTag("Player").GetComponent<KarlController>();
       // inven = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        displayControl = GameObject.FindGameObjectWithTag("DisplayControl").GetComponent<DisplayControl>();
        displayControl.ActivateDisplay();
    }

    public void DisplayLetter()
    {
        displayControl.DisplayOff = true;
        displayControl.ActivateDisplay();
        karl.CheckOn();
    }

}
