using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControl : MonoBehaviour
{
    public bool DisplayOff;
    public LetterDisplay letterDisplay;

    private void Start()
    {
        DisplayOff = false;
        letterDisplay.DisplayLetter();
    }

    public void ActivateDisplay()
    {
        if (DisplayOff == false)
        {
            DisplayOff = true;
            letterDisplay.latterOn = DisplayOff;
            letterDisplay.DisplayLetter();
        }
        else if (DisplayOff == true)
        {
            DisplayOff = false;
            letterDisplay.latterOn = DisplayOff;
            letterDisplay.DisplayLetter();
        }
    }
}
