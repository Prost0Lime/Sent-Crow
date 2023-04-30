using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        slider.value = GameObject.FindGameObjectWithTag("AM").GetComponent<VolumeValue>().musicVolume;
    }
}
