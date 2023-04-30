using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    public AudioSource[] AudioTheme;
    public float musicVolume = 1f;

    private void Update()
    {
        for (int i = 0; i < AudioTheme.Length; i++)
        {
            AudioTheme[i].volume = musicVolume;
        }
    }


    public void SetVolume(float vol)
    {
        musicVolume = vol;
        
    }
}
