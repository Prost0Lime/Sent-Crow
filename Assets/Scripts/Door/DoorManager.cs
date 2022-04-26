using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public bool triggerOpen;

    public void Start()
    {
        triggerOpen = false;
    }

    public void TapToDoor()
    {
        triggerOpen = true;
        Invoke("StatusToNull", 1f);   
    }

    public void StatusToNull()
    {
        triggerOpen = false;
    }
}
