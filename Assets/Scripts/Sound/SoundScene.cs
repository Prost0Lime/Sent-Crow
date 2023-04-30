using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScene : MonoBehaviour
{
    [HideInInspector]
    public SoundManager soundManager;
    public AudioClip ThemeGlobal;
    public AudioClip ThemeSecond;

    public bool useTheme;


    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("AM").GetComponent<SoundManager>();
        soundManager.AudioTheme.clip = ThemeGlobal;
        soundManager.AudioTheme.Play();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (useTheme == true) //если тру, то использовать альтернативное аудио 
            {
                soundManager.AudioTheme.clip = ThemeSecond;
                soundManager.AudioTheme.Play();
            }
            else // иначе используется дополнительный аудиосаунд
            {
                soundManager.AudioSound2.clip = ThemeSecond;
                soundManager.AudioSound2.Play();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (useTheme == true)
            {
                soundManager.AudioTheme.clip = ThemeGlobal;
                soundManager.AudioTheme.Play();
            }
        }
    }

}
