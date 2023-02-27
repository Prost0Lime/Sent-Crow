using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public KarlController Karl;
    private bool Lamp;

    void Start()
    {
        Karl = GameObject.FindGameObjectWithTag("Player").GetComponent<KarlController>();
        Lamp = false;
    }

    public void LampChange()
    {
        if (Lamp == false)
        {
            Lamp = true;
            Karl.LampOn();
        }
        else if (Lamp == true)
        {
            Lamp = false;
            Karl.LampOff();
        }
    }
}
